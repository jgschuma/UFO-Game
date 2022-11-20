using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float parTimeInSeconds;
    public float timeMultiplierForPoints;
    public Text timerText;
    public bool timerOn = true;
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
        if (timerOn)
        {
            milliseconds += Time.deltaTime * 1000;
            if (milliseconds >= 1000)
            {
                seconds += 1;
                milliseconds -= 1000;
            }
            if (Mathf.FloorToInt(seconds) >= 60)
            {
                minutes++;
                seconds -= 60;
            }
            UpdateTimer();
        }
    }

    void UpdateTimer(){
        timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + ((int)milliseconds/10).ToString("D2");
    }

    public float CalculateCurrentTime(){
        float currentTime = 0;

        currentTime += (minutes * 60);
        currentTime += seconds;
        currentTime += (float)(milliseconds * 0.001);

        return currentTime;
    }

    void AddTimeBonus(bool endedDueToDeath){
        float totalTime = CalculateCurrentTime();

        float timeDifference = parTimeInSeconds - totalTime;

        if (timeDifference > 0 && !endedDueToDeath){
            AustinEventManager.ScorePoints((int)Mathf.Floor(timeDifference * timeMultiplierForPoints));
        } else {
            //0 points lmao
        }
        AustinEventManager.CalcFinished("speedBonusCalc");
    }
}
