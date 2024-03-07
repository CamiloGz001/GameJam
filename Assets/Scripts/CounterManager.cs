using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.UI;

public class CounterManager : MonoBehaviour
{
    public TextMeshProUGUI totalDucksText;
    public TextMeshProUGUI ducksDiedText;
    public TextMeshProUGUI ducksFeedText;
    public int totalDucks;
    public int ducksDied;
    public int ducksFeed;
    public DuckMovement duck;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalDucks = FindObjectsOfType<DuckMovement>().Length;
        totalDucksText.text = "x" + totalDucks.ToString();
        ducksDiedText.text = "x" + ducksDied.ToString();
        ducksFeedText.text = "x" + ducksFeed.ToString();
    }

    void DuckFeeded()
    {
        if(duck.isItFull)
        {
            //totalDucks
        }
        
    }
}
