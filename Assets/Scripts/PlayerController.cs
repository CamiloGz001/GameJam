using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public GameObject Duck;
    public TextMeshProUGUI callText;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed * Time.deltaTime);
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput * Time.deltaTime);   
         
    }

    void OnTriggerStay(Collider other){
        if (other.CompareTag("duck"))
        {
            callText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        callText.gameObject.SetActive(false);
    }
    
}
