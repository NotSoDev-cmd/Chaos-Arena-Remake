using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    public Slider filled;

    // Start is called before the first frame update
    void Start()
    {
        filled.maxValue = 1f;
        filled.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(float perc) {
        //Debug.Log(perc);
        filled.value = perc;
    }
}
