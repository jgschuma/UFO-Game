using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    [Header("Current Scores")]
    private int score;
    private int highScore;

    [Header("Player Pref Keys")]
    private string highScoreKey = "hiScore";

    //Start method
    void Start(){
        PlayerPrefs.SetInt(highScoreKey, 0); //resets the high score in the beginning. Testing only.
        LoadHiScore();
        
    }
    void Update(){
        DisplayScore();
        CheckNewHighScore();
    }

    //Sub to events
    private void OnEnable() {
        AustinEventManager.onStartGame += ResetScore;
        AustinEventManager.onPlayerDeath += CheckNewHighScore;
        AustinEventManager.onScorePoints += AddScore;
    }

    //Unsub from events
    private void OnDisable() {
        AustinEventManager.onStartGame -= ResetScore;
        AustinEventManager.onPlayerDeath -= CheckNewHighScore;
        AustinEventManager.onScorePoints -= AddScore;
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

    //Loading the high score and displaying it
    void LoadHiScore(){
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        DisplayHighScore();
    }

    //Check to see if the score is greater than the high score, if so make it the new high score and write that to prefs
    void CheckNewHighScore(){
        if (score > highScore){
            PlayerPrefs.SetInt(highScoreKey, score);
            //updating the high score in game in real time
            highScore = score;
            DisplayHighScore();
        }
    }

    //Display the high score
    void DisplayHighScore(){
        highScoreText.text = highScore.ToString();
    }
}
