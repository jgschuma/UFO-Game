using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool needNewName = false;
    public int newNamePos = 0;
    private GameObject toChange;
    private InputField inField;

    private GameObject mainMenu;
    private GameObject optionsMenu;
    private GameObject scoreMenu;
    

    private Text HighName1;
    private Text HighName2;
    private Text HighName3;
    private string highScore1NameKey = "hiScoreName1";
    private string highScore2NameKey = "hiScoreName2";
    private string highScore3NameKey = "hiScoreName3";

    //Sub to events
    private void OnEnable() {
        AustinEventManager.onPlayerDeath += GameOver;
        AustinEventManager.onNewHighScore += SetNewName;
    }

    //Unsub from events
    private void OnDisable() {
        AustinEventManager.onPlayerDeath -= GameOver;
        AustinEventManager.onNewHighScore -= SetNewName;
    }

    void Awake(){

        if (Instance == null){
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
       
        DontDestroyOnLoad(gameObject);
        mainMenu = GameObject.Find("MainCanvas/MainMenu");
        optionsMenu = GameObject.Find("MainCanvas/OptionsMenu");
        scoreMenu = GameObject.Find("MainCanvas/Scores");
        HighName1 = GameObject.Find("MainCanvas/Scores/Canvas/HighName1").GetComponent<Text>();
        HighName2 = GameObject.Find("MainCanvas/Scores/Canvas/HighName2").GetComponent<Text>();
        HighName3 = GameObject.Find("MainCanvas/Scores/Canvas/HighName3").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadHighScoreNames();
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        scoreMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void GameOver(){
        StartCoroutine(Reset());
    }

    void GetNewName(){
        inField.text = "   ";
        inField.Select();
        inField.interactable = true;
        inField.onEndEdit.AddListener(delegate{FinishNewName();});
    }

    void SetNewName(int score, int pos){
        needNewName = true;
        newNamePos = pos;
    }

    public void FinishNewName(){
        inField.interactable = false;
        Debug.Log("Saving POSs");
        SaveHighScoreNames();
    }

    public void LoadHighScoreNames(){
        HighName1.text = PlayerPrefs.GetString(highScore1NameKey);
        Debug.Log("Loaded pos1 as " + HighName1.text);
        HighName2.text = PlayerPrefs.GetString(highScore2NameKey);
        HighName3.text = PlayerPrefs.GetString(highScore3NameKey);
    }
    public void SaveHighScoreNames(){
        PlayerPrefs.SetString(highScore1NameKey, inField.text);
        Debug.Log("Saved pos1 as " + HighName1.text);
        PlayerPrefs.SetString(highScore2NameKey, HighName2.text);
        PlayerPrefs.SetString(highScore3NameKey, HighName3.text);
    }

    IEnumerator Reset() {
        yield return new WaitForSeconds(3);
        //load main menu
        var op = SceneManager.LoadSceneAsync(0);
        op.completed += (x) => {
            mainMenu = GameObject.Find("MainCanvas/MainMenu");
            optionsMenu = GameObject.Find("MainCanvas/OptionsMenu");
            scoreMenu = GameObject.Find("MainCanvas/Scores");

            if (needNewName){
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            scoreMenu.SetActive(true);
            toChange = GameObject.Find("MainCanvas/Scores/Canvas/HighName" + newNamePos);
            inField = toChange.GetComponent<InputField>();
            GetNewName();
            } else {
                mainMenu.SetActive(true);
                optionsMenu.SetActive(false);
                scoreMenu.SetActive(false);
            }
        };
    }
}
