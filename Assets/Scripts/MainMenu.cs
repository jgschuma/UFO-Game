using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public AudioSource menuMusicSource;

    public Slider volumeSlider;

    void Start(){
        if (!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        Load();
        menuMusicSource.Play();
    }

    void Update(){
        ChangeVolume();
    }

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AustinEventManager.StartGame();
    }

    public void QuitGame(){
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;

    }

    public void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
