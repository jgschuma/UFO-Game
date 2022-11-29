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
    private InputField inField;

    private GameObject mainMenu;
    private GameObject optionsMenu;
    private GameObject scoreMenu;
    private GameObject creditsMenu;

    
    

    private Text HighName1;
    private Text HighName2;
    private Text HighName3;
    private string highScore1NameKey = "hiScoreName1";
    private string highScore2NameKey = "hiScoreName2";
    private string highScore3NameKey = "hiScoreName3";

    //Sub to events
    private void OnEnable() {
        AustinEventManager.onNewHighScore += SetNewName;
        AustinEventManager.onFinishScoreCalc += ResetTime;
    }

    //Unsub from events
    private void OnDisable() {
        AustinEventManager.onNewHighScore -= SetNewName;
        AustinEventManager.onFinishScoreCalc -= ResetTime;
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
        creditsMenu = GameObject.Find("MainCanvas/Credits");
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
        creditsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ResetTime(){
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
        PlayerPrefs.SetString(highScore1NameKey, inField.text);
        Debug.Log("Saving POSs");
        SaveHighScoreNames();

    }

    public void LoadHighScoreNames(){
        HighName1.GetComponent<InputField>().text = PlayerPrefs.GetString(highScore1NameKey);
        Debug.Log("Loaded pos1 as " + HighName1.text + "\n" + PlayerPrefs.GetString(highScore1NameKey));
        HighName2.GetComponent<InputField>().text = PlayerPrefs.GetString(highScore2NameKey);
        HighName3.GetComponent<InputField>().text = PlayerPrefs.GetString(highScore3NameKey);
    }
    public void SaveHighScoreNames(){
        PlayerPrefs.SetString(highScore1NameKey, HighName1.text);
        Debug.Log("Saved pos1 as " + HighName1.text);
        Debug.Log("Proof: " + PlayerPrefs.GetString(highScore1NameKey));
        PlayerPrefs.SetString(highScore2NameKey, HighName2.text);
        Debug.Log("Saved pos2 as " + HighName2.text);
        Debug.Log("Proof: " + PlayerPrefs.GetString(highScore2NameKey));
        PlayerPrefs.SetString(highScore3NameKey, HighName3.text);
        Debug.Log("Saved pos3 as " + HighName3.text);
        Debug.Log("Proof: " + PlayerPrefs.GetString(highScore3NameKey));
    }

    IEnumerator Reset() {
        yield return new WaitForSeconds(3);
        //load main menu
        var op = SceneManager.LoadSceneAsync(0);
        op.completed += (x) => {
            mainMenu = GameObject.Find("MainCanvas/MainMenu");
            optionsMenu = GameObject.Find("MainCanvas/OptionsMenu");
            scoreMenu = GameObject.Find("MainCanvas/Scores");
            creditsMenu = GameObject.Find("MainCanvas/Credits");
            HighName1 = GameObject.Find("MainCanvas/Scores/Canvas/HighName1").GetComponent<Text>();
            HighName2 = GameObject.Find("MainCanvas/Scores/Canvas/HighName2").GetComponent<Text>();
            HighName3 = GameObject.Find("MainCanvas/Scores/Canvas/HighName3").GetComponent<Text>();
            HighName1.GetComponent<InputField>().text = PlayerPrefs.GetString(highScore1NameKey);
            HighName2.GetComponent<InputField>().text = PlayerPrefs.GetString(highScore2NameKey);
            HighName3.GetComponent<InputField>().text = PlayerPrefs.GetString(highScore3NameKey);

            if (needNewName){
            needNewName = false;
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            scoreMenu.SetActive(true);
            creditsMenu.SetActive(false);
            Debug.Log(newNamePos);
            if (newNamePos == 1){
                Debug.Log("High Score in pos 1");
                inField = HighName1.GetComponent<InputField>();
                HighName3.GetComponent<InputField>().text = HighName2.GetComponent<InputField>().text;
                HighName2.GetComponent<InputField>().text = HighName1.GetComponent<InputField>().text;
            } else if (newNamePos == 2){
                Debug.Log("High Score in pos 2");
                inField = HighName2.GetComponent<InputField>();
                HighName3.GetComponent<InputField>().text = HighName2.GetComponent<InputField>().text;
            } else if (newNamePos == 3){
                Debug.Log("High Score in pos 3");
                inField = HighName3.GetComponent<InputField>();
            }
            GetNewName();
            } else {
                mainMenu.SetActive(true);
                optionsMenu.SetActive(false);
                scoreMenu.SetActive(false);
                creditsMenu.SetActive(false);
            }
        };
    }
}
