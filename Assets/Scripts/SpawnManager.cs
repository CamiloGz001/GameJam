using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject DuckPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDuck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnDuck(){
        while(true){
            yield return new WaitForSeconds(5.0f);
            Instantiate(DuckPrefab);
        }
       
    }
}
