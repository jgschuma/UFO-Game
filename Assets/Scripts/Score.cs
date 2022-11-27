using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    [Header("Current Scores")]
    private int score;
    private int highScore1;
    private int highScore2;
    private int highScore3;

    [Header("Player Pref Keys")]
    private string highScore1Key = "hiScore1";
    private string highScore2Key = "hiScore2";
    private string highScore3Key = "hiScore3";


    [Header("Wrapping")]
    private bool healthCalcDone = false;
    private bool speedBonusCalcDone = false;

    //Start method
    void Start(){
        LoadHiScore();
        
    }
    void FixedUpdate(){
        DisplayScore();
        if (healthCalcDone && speedBonusCalcDone){
            healthCalcDone = false;
            speedBonusCalcDone = false;
            CheckNewHighScore();
        }
    }

    //Sub to events
    private void OnEnable() {
        AustinEventManager.onStartGame += ResetScore;
        AustinEventManager.onScorePoints += AddScore;
        AustinEventManager.onCalcDone += CalcVariables;
    }

    //Unsub from events
    private void OnDisable() {
        AustinEventManager.onStartGame -= ResetScore;
        AustinEventManager.onScorePoints -= AddScore;
        AustinEventManager.onCalcDone -= CalcVariables;
    }

    void CalcVariables(string whatFinished){
        if (whatFinished == "healthCalc"){
            healthCalcDone = true;
        } else if (whatFinished == "speedBonusCalc"){
            speedBonusCalcDone = true;
        } else{
            Debug.Log("Typo in whatFinished");
        }

    }

    //Resets the players score, and displays it
    void ResetScore(){
        score = 0;
        DisplayScore();
    }

    void AddScore(int amt){
        score += amt;
    }

    //Displaying the player's score
    void DisplayScore(){
        scoreText.text = score.ToString();
    }

    void LoadHiScore(){
        highScore1 = PlayerPrefs.GetInt(highScore1Key);
        highScore2 = PlayerPrefs.GetInt(highScore2Key);
        highScore3 = PlayerPrefs.GetInt(highScore3Key);
    }

    void SaveHiScore(){
        PlayerPrefs.SetInt(highScore1Key, highScore1);
        PlayerPrefs.SetInt(highScore2Key, highScore2);
        PlayerPrefs.SetInt(highScore3Key, highScore3);

    }

    void CheckNewHighScore(){
        Debug.Log("Checking High Score");
        if (score == 0){
            Debug.Log("Score: 0");
        }
        if (score > highScore3 && score < highScore2){
            AustinEventManager.NewHighScore(score, 3);
            highScore3 = score;
        } else if (score > highScore2 && score < highScore1){
            AustinEventManager.NewHighScore(score, 2);
            highScore3 = highScore2;
            highScore2 = score;
        } else if (score > highScore1){
            AustinEventManager.NewHighScore(score, 1);
            highScore3 = highScore2;
            highScore2 = highScore1;
            highScore1 = score;
            Debug.Log("BingoHighScore");
        }
        SaveHiScore();
        AustinEventManager.FinishCalcAllScores();
    }
}
