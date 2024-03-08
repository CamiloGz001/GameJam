using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        playerAnim = GetComponent<Animator>();
    }
<<<<<<< Updated upstream
void Update()
=======

    void Update()
>>>>>>> Stashed changes
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBoosting = true;
<<<<<<< Updated upstream
        }
=======
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBoosting = false;
        }

        if (isBoosting)
        {
            // Aumentar la velocidad mientras se mantiene presionada la tecla Espacio
            speed = 15;
        }
        else
        {
            // Restaurar la velocidad inicial si la tecla Espacio no está presionada
            speed = initialSpeed;
        }
    }
>>>>>>> Stashed changes

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBoosting = false;
        }

        if (isBoosting)
        {
            // Aumentar la velocidad mientras se mantiene presionada la tecla Espacio
            speed = 15;
        }
        else
        {
            // Restaurar la velocidad inicial si la tecla Espacio no está presionada
            speed = initialSpeed;
        }
    }
void Movement()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 forwardForce = focalPoint.transform.forward * forwardInput * speed * Time.deltaTime;
        Vector3 horizontalForce = focalPoint.transform.right * horizontalInput * speed * Time.deltaTime;

        // Aplicar fuerzas al jugador solo si hay entrada de movimiento
        if (forwardInput != 0 || horizontalInput != 0)
        {
            // Agregar fuerzas de movimiento
            playerRb.AddForce(forwardForce, ForceMode.VelocityChange);
            playerRb.AddForce(horizontalForce, ForceMode.VelocityChange);

            // Rotar el jugador hacia la dirección de movimiento
            Quaternion newRotation = Quaternion.LookRotation(forwardForce + horizontalForce);
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
<<<<<<< Updated upstream
private void OnTriggerEnter(Collider other) {
=======

    private void OnTriggerEnter(Collider other) {
>>>>>>> Stashed changes
        if(other.CompareTag("Food")){
            hasFood = true;
            food.gameObject.SetActive(true);
        }
    }
}
