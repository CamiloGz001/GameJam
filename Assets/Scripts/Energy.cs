using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Energy : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public void SetMaxEnergy(int energy){
        slider.maxValue = energy;
        slider.value = energy;
    }
    public void SetEnergy(int energy){
        slider.value = energy;
    }
}
