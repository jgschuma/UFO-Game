using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public AudioSource menuMusicSource;

    public Slider volumeSlider;

    private GameObject mainMenu;
    private GameObject optionsMenu;
    private GameObject scoreMenu;

    public Text highScore1Text;
    public Text highScore2Text;
    public Text highScore3Text;
    public Text highScore1NameText;
    public Text highScore2NameText;
    public Text highScore3NameText;

    private string highScore1Key = "hiScore1";
    private string highScore2Key = "hiScore2";
    private string highScore3Key = "hiScore3";

    private string highScore1NameKey = "hiScoreName1";
    private string highScore2NameKey = "hiScoreName2";
    private string highScore3NameKey = "hiScoreName3";

    void Awake(){
        mainMenu = GameObject.Find("MainCanvas/MainMenu");
        optionsMenu = GameObject.Find("MainCanvas/OptionsMenu");
        scoreMenu = GameObject.Find("MainCanvas/Scores");
    }

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
        mainMenu.SetActive(true);
        optionsMenu.SetActive(true);
        scoreMenu.SetActive(true);
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
        //Loading Options Menu
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

        //Loading High Scores
        highScore1NameText.text = PlayerPrefs.GetString(highScore1NameKey);
        highScore2NameText.text = PlayerPrefs.GetString(highScore2NameKey);
        highScore3NameText.text = PlayerPrefs.GetString(highScore3NameKey);
        highScore1Text.text = PlayerPrefs.GetInt(highScore1Key).ToString();
        highScore2Text.text = PlayerPrefs.GetInt(highScore2Key).ToString();
        highScore3Text.text = PlayerPrefs.GetInt(highScore3Key).ToString();
    }

    public void ClearHighScore(){
        PlayerPrefs.SetInt(highScore1Key, 0);
        PlayerPrefs.SetInt(highScore2Key, 0);
        PlayerPrefs.SetInt(highScore3Key, 0);
    }
}
