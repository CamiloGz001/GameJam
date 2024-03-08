using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAction : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip shoot;
    public float killInterval;
    public float initialDelay;
    public CounterManager counterManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        counterManager = GameObject.Find("GameManager").GetComponent<CounterManager>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("KillDuck", initialDelay, killInterval);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void KillDuck(){
        GameObject[] ducks = GameObject.FindGameObjectsWithTag("duck");
        if(ducks.Length > 0){
            int index = Random.Range(0, ducks.Length);
            audioSource.PlayOneShot(shoot);
            Destroy(ducks[index]);
            counterManager.DuckDied();
        }

    }
}
