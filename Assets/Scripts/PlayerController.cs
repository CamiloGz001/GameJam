using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public float initialSpeed = 10;
    private GameObject focalPoint;

     public GameObject food;
    public bool hasFood;
    [SerializeField] private Animator playerAnim;
    private bool isBoosting = false;

    DuckMovement duckMOvement;
    public TextMeshProUGUI callText;
    bool canCall = false;
    public bool canFeed = false;

    public CounterManager counterManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        playerAnim = GetComponent<Animator>();
        counterManager = GameObject.Find("GameManager").GetComponent<CounterManager>();
    }

    void Update()
    {
        Movement();
        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBoosting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isBoosting = false;
        }

        if (isBoosting)
        {
            // Aumentar la velocidad mientras se mantiene presionada la tecla Espacio
            speed = 0.5f;
        }
        else
        {
            // Restaurar la velocidad inicial si la tecla Espacio no está presionada
            speed = initialSpeed;
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && canCall && duckMOvement!= null)
        {
            duckMOvement.GoToPlayer(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.E) && canFeed)
        {
            ToFeed(duckMOvement);
        }
    }
     void Movement()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;

        Vector3 movementDirection = cameraForward * forwardInput + Camera.main.transform.right * horizontalInput;
        movementDirection.Normalize();

        playerRb.AddForce(movementDirection * speed, ForceMode.Impulse);
        // Aplicar fuerzas al jugador solo si hay entrada de movimiento
        if (forwardInput != 0 || horizontalInput != 0)
        {
            // Rotar el jugador hacia la dirección de movimiento
            Quaternion newRotation = Quaternion.LookRotation(cameraForward + movementDirection);
            playerRb.MoveRotation(newRotation);

            // Animación del jugador
            playerAnim.SetBool("Walking", true);
        }
        else
        {
            // Detener la animación si no hay movimiento
            playerAnim.SetBool("Walking", false);
        }
    }
     void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("duck"))
        {
            duckMOvement = other.GetComponent<DuckMovement>();
            callText.gameObject.SetActive(true);
            canCall = true;

        }

        if (Vector3.Distance(transform.position, other.transform.position) < 0.6)
        {
            canFeed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        duckMOvement = null;
        callText.gameObject.SetActive(false);
        canCall = false;
        canFeed = false;
    }

    void ToFeed(DuckMovement duckMovement)
    {
        duckMovement.wait = false;
        duckMovement.isItFull = true;
        counterManager.DuckFeeded();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Food")){
            hasFood = true;
            food.gameObject.SetActive(true);
        }
    }
}