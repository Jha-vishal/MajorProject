using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalTime;
    private float minutes;
    private float seconds;

    public Text CountDownTimer;
   
    void Update()
    {
        
        totalTime -= Time.deltaTime;

        minutes = (int)(totalTime / 60);
        seconds = (int)(totalTime % 60);


        CountDownTimer.text = "Time\n" + minutes.ToString() + ":" + seconds.ToString(); 
        
    }
}
