using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public float initialSpeed =500;
    private GameObject focalPoint;
    public int maxEnergy = 100;
    public int currentEnergy;
    public Energy energyBar;
    [SerializeField] private Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);

        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            speed=1000;
            playerRb.AddForce(focalPoint.transform.forward * speed * Time.deltaTime);
            GetTired();
        }
        Recover();
        speed=initialSpeed;
         
    }

    void Movement()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput); 

        //PlayerAnimation
        if(forwardInput != 0|| horizontalInput != 0)
        {
            playerAnim.SetBool("Walking", true);
        } else 
        {
            playerAnim.SetBool("Walking", false);
        }
    }
    void GetTired(){
        for(int i=0;i<maxEnergy;i++)
        {
            currentEnergy -=i;
            energyBar.SetEnergy(currentEnergy);
            if(currentEnergy== 0){
                speed = 250;
            }
        }
    }
    void Recover(){
        for(int i=0;i<maxEnergy;i++)
        {
            currentEnergy +=i;
            energyBar.SetEnergy(currentEnergy);
        }
    }
}
