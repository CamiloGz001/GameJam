using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            playerRb.AddForce(focalPoint.transform.forward * speed * Time.deltaTime);
            GetTired();
        }
        
        Recover();
        speed = initialSpeed;
    }

    void Movement()
{
    float forwardInput = Input.GetAxis("Vertical");
    float horizontalInput = Input.GetAxis("Horizontal");
    Vector3 forwardForce = focalPoint.transform.forward * forwardInput * speed;
    Vector3 horizontalForce = focalPoint.transform.right * horizontalInput * speed;

    playerRb.AddForce(forwardForce);
    playerRb.AddForce(horizontalForce);

    // Rotación del jugador
    if (forwardInput != 0 || horizontalInput != 0)
    {
        Quaternion newRotation = Quaternion.LookRotation(forwardForce + horizontalForce);
        playerRb.MoveRotation(newRotation);
    }

    //PlayerAnimation
    if (forwardInput != 0 || horizontalInput != 0)
    {
        playerAnim.SetBool("Walking", true);
    }
    else
    {
        playerAnim.SetBool("Walking", false);
    }
}

    void GetTired()
    {
        for (int i = 0; i < maxEnergy; i++)
        {
            currentEnergy -= i;
            energyBar.SetEnergy(currentEnergy);
            if (currentEnergy == 0)
            {
                speed = 250;
            }
        }
    }

    void Recover()
    {
        for (int i = 0; i < maxEnergy; i++)
        {
            currentEnergy += i;
            energyBar.SetEnergy(currentEnergy);
        }
    }
}
