using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public float initialSpeed = 11;
     public GameObject food;
     public GameObject focalPoint;
    public bool hasFood;
    [SerializeField] private Animator playerAnim;
    private bool isBoosting = false;
    DuckMovement duckMOvement;
    public TextMeshProUGUI callText;
    bool canCall = false;
    public bool canFeed = false;
    public CounterManager counterManager;
    public float jumpForce = 4f;
    public bool isOnGround = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
            speed = 12;
        }
        else
        {
            // Restaurar la velocidad inicial si la tecla Espacio no está presionada
            speed = initialSpeed;
        }
        

        if (Input.GetKeyDown(KeyCode.Q) && canCall && duckMOvement!= null)
        {
            duckMOvement.GoToPlayer(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.E) && canFeed)
        {
            ToFeed(duckMOvement);
        }
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround){
            Jump();
        }
        
    }
     void Movement()
{
    float forwardInput = Input.GetAxis("Vertical");
    float horizontalInput = Input.GetAxis("Horizontal");

    Vector3 cameraForward = Camera.main.transform.forward;
    cameraForward.y = 0f; // Mantenemos la componente y en cero para evitar movimientos verticales

    Vector3 movementDirection = cameraForward * forwardInput + Camera.main.transform.right * horizontalInput;
    movementDirection.Normalize(); // Normalizamos el vector de dirección para evitar movimientos diagonales más rápidos

    // Aplicar fuerzas al jugador solo si hay entrada de movimiento
    if (forwardInput != 0 || horizontalInput != 0)
    {
        // Calculamos la fuerza de movimiento multiplicando la dirección por la velocidad
        Vector3 movementForce = movementDirection * speed * Time.deltaTime;
        
        // Aplicamos la fuerza al jugador
        playerRb.AddForce(movementForce, ForceMode.VelocityChange);

        // Rotar el jugador hacia la dirección de movimiento
        Quaternion newRotation = Quaternion.LookRotation(movementDirection);
        playerRb.MoveRotation(newRotation);

        // Activar la animación del jugador
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
        canCall = false;
        canFeed = false;
    }

    void ToFeed(DuckMovement duckMovement)
    {
        duckMovement.wait = false;
        duckMovement.isItFull = true;
        counterManager.DuckFeeded();
    }

    void Jump(){
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
        } 
    }
}