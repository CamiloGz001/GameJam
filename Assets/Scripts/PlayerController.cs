using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public float initialSpeed = 500;
    private GameObject focalPoint;
    public int maxEnergy = 100;
    public int currentEnergy;
    public Energy energyBar;
    [SerializeField] private Animator playerAnim;

    private bool isBoosting = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);

        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBoosting = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBoosting = false;
        }

        Recover();
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

    void FixedUpdate()
    {
        if (isBoosting && currentEnergy > 0)
        {
            // Aumentar la velocidad mientras se mantiene presionada la tecla Espacio
            speed += 1;

            // Disminuir la energía
            currentEnergy--;
            energyBar.SetEnergy(currentEnergy);
        }
        else
        {
            // Restaurar la velocidad inicial si la tecla Espacio no está presionada
            speed = initialSpeed;
        }
    }

    void Recover()
    {
        // Recuperar la energía con el tiempo
        currentEnergy++;
        energyBar.SetEnergy(currentEnergy);

        // Asegurarse de que la energía no supere el máximo
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }
}
