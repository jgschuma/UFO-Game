using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float milliseconds;
    public int seconds;
    public int minutes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        milliseconds += Time.deltaTime * 1000;
        if (milliseconds >=1000){
            seconds += 1;
            milliseconds -= 1000;
        }
        if (Mathf.FloorToInt(seconds) >= 60){
            minutes++;
            seconds -= 60;
        }
        UpdateTimer();
    }

    void UpdateTimer(){
        timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + ((int)milliseconds).ToString("D3");
    }
}
