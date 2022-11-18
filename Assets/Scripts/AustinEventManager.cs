using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AustinEventManager : MonoBehaviour
{
    public delegate void StartGameDelegate();
    public static StartGameDelegate onStartGame;

    public delegate void EndGameDelegate(bool endedDueToDeath);
    public static EndGameDelegate onGameOver;

    public delegate void FinishCalcAllScoresDelegate();
    public static FinishCalcAllScoresDelegate onFinishScoreCalc;

    public delegate void calcFinishDelegate(string whatFinished);
    public static calcFinishDelegate onCalcDone;

    public delegate void ShowScoreDelegate();
    public static ShowScoreDelegate onNeedScorePage;

    public delegate void NewHighScoreDelegate(int newScore, int pos);
    public static NewHighScoreDelegate onNewHighScore;

    public delegate void ScorePointsDelegate(int amt);
    public static ScorePointsDelegate onScorePoints;


    public static void StartGame(){
        if (onStartGame != null){
            onStartGame();
        }
    }

    public static void GameOver(bool endedDueToDeath){
        if (onGameOver != null){
            onGameOver(endedDueToDeath);
        }
    }

    // When all calculations are done
    public static void FinishCalcAllScores(){
        if (onFinishScoreCalc != null){
            onFinishScoreCalc();
        }
    }

    public static void ScorePoints(int score){
        if (onScorePoints != null){
            onScorePoints(score);
        }
    }

    public static void NewHighScore(int newScore, int pos){
        if (onNewHighScore != null){
            onNewHighScore(newScore, pos);
        }
    }

    // When one calculation is done
    public static void CalcFinished(string whatFinished){
        if (onCalcDone != null){
            onCalcDone(whatFinished);
        }
    }
}
