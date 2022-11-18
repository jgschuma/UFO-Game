using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float parTimeInSeconds;
    public float timeMultiplierForPoints;
    public Text timerText;
    private float milliseconds;
    private int seconds;
    private int minutes;

    void OnEnable(){
        AustinEventManager.onGameOver += AddTimeBonus;
    }

    void OnDisable(){
        AustinEventManager.onGameOver -= AddTimeBonus;
    }

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
        timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + ((int)milliseconds/10).ToString("D2");
    }

    float CalculateTotalTime(){
        float totalTime = 0;

        totalTime += (minutes * 60);
        totalTime += seconds;
        totalTime += (float)(milliseconds * 0.001);

        return totalTime;
    }

    void AddTimeBonus(){
        float totalTime = CalculateTotalTime();

        float timeDifference = parTimeInSeconds - totalTime;

        if (timeDifference > 0){
            AustinEventManager.ScorePoints((int)Mathf.Floor(timeDifference * timeMultiplierForPoints));
        } else {
            //0 points lmao
        }
        AustinEventManager.CalcFinished("speedBonusCalc");
    }
}
