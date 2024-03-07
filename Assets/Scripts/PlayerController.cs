using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 500;
    private GameObject focalPoint;
    public int maxEnergy = 100;
    public int currentEnergy;
    public Energy energyBar;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput); 

        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            speed=1000;
            playerRb.AddForce(focalPoint.transform.forward * speed * Time.deltaTime);
            GetTired();
        }
        Recover();
        speed=500;
        
        
         
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
