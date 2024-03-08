using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    public TextMeshProUGUI totalDucksText;
    public TextMeshProUGUI ducksDiedText;
    public TextMeshProUGUI ducksFeedText;
    public int totalDucks;
    public int ducksDied;
    public int ducksFeed;
    public DuckMovement[] duck;
    public HunterAction hunt;
    
    void Start()
    {
        hunt = GetComponent<HunterAction>();
        duck = FindObjectsOfType<DuckMovement>();
        hunt = FindObjectsOfType<HunterAction>()[0]; // Assuming first HunterAction found
        //totalDucks = duck.Length;
    }

    // Update is called once per frame
    void Update()
    {
        totalDucks = FindObjectsOfType<DuckMovement>().Length;
        totalDucksText.text = "x" + totalDucks.ToString();
        ducksDiedText.text = "x" + ducksDied.ToString();
        ducksFeedText.text = "x" + ducksFeed.ToString();
        
    }

    public void DuckFeeded()
    {
        ducksFeed++;
    }

    public void DuckDied()
    {
        ducksDied++;
    }
}
