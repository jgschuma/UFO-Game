using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioSource menuMusicSource;

    void Start(){
        menuMusicSource.Play();
    }

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AustinEventManager.StartGame();
    }

    public void QuitGame(){
        Debug.Log("Quit!");
        Application.Quit();
    }
}
