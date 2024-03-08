using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    private CharacterController controller;
    DuckMovement duckMOvement;
 // Referencia al CharacterController
    public float speed = 5f; // Velocidad de movimiento del personaje
    public TextMeshProUGUI callText;
    bool canCall = false;
    public bool canFeed = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        // duckMOvement = GameObject.Find("Duck").GetComponent<DuckMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

        // Mover el personaje con el CharacterController
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && canCall && duckMOvement!= null)
        {
            duckMOvement.GoToPlayer(transform.position);

        }

        if (Input.GetKeyDown(KeyCode.E) && canFeed)
        {
            ToFeed(duckMOvement);

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
    }
}
