using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DuckMovement : MonoBehaviour
{
    private Collider patrolArea; // Área de patrulla definida
    public Vector3 randomPoint;
    public float speed;
    public bool isItFull = false;
    public bool wait { get; set; } = false;

    void Start()
    {
        patrolArea = GameObject.Find("Patrol").GetComponent<BoxCollider>();
        SetRandomDestination();

    }

    // Update is called once per frame
    void Update()
    {
        if(isItFull){
            StartCoroutine(DestroyPajaro());
        }

        if (Vector3.Distance(transform.position, randomPoint) < 0.6 && !wait)
        {
            SetRandomDestination();
        }

        Vector3 direction = (randomPoint - transform.position).normalized;

        // Rotar el pájaro hacia la dirección de movimiento
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }


        transform.position = Vector3.MoveTowards(transform.position, randomPoint, speed * Time.deltaTime);


    }

    void SetRandomDestination()
    {
        // Generar un punto aleatorio dentro del área de patrulla
        randomPoint = RandomPointInBounds(patrolArea.bounds);

    }

    // Método para generar un punto aleatorio dentro de los límites del área de patrulla
    Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            7,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void GoToPlayer(Vector3 playerPosition)
    {
        if(!isItFull){
            randomPoint = playerPosition;
            wait = true;
        }
        return;
    }

    IEnumerator DestroyPajaro(){
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }


}
