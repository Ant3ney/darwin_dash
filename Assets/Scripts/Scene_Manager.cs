using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Scene_Manager : MonoBehaviour
{
    [HideInInspector]
    public static Scene_Manager Instance { get { return _instance; } }
   // Implement a singleton pattern
    private static Scene_Manager _instance;

    // format for mathtype: "mathType_numDigits"
    [HideInInspector]
    public static string mathType = "addition_1";
    [HideInInspector]
    public static float highScore = 0;
    static string HIGH_SCORE_KEY = "highScore";
    [HideInInspector]
    public static float math_time ;
    [HideInInspector]
    public static float platformer_time = -3;
    
    public Text timeText;

    private void Awake()
    {
        manageSingleton();
    }

    public static void add_time(float time)
    {
            platformer_time += time;
    
    } 

    public static void getTime(string gameType)
    {
        if (gameType == "math")
        {
            math_time = PlayerPrefs.GetFloat("math_time");
        }
        else if (gameType == "platformer")
        {
            platformer_time = PlayerPrefs.GetFloat("platformer_time");
        }
    }

    void manageSingleton(){
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

   

    void Update()
    {

        if (platformer_time == -3) {
            platformer_time = 140f;
        }
        if (platformer_time < 1 && platformer_time >0) {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
        if (platformer_time > 0)
        { platformer_time -= Time.deltaTime;
        }
        else
        {
        }

        DisplayTime(platformer_time);


    }

    void DisplayTime(float time_edit)
    {
        if (time_edit < 0)
        {
            time_edit = 0;
        }

        float Minuites = Mathf.FloorToInt(time_edit / 60);
        float Seconds = Mathf.FloorToInt(time_edit % 60);

        timeText.text = string.Format("{0:00}:{1:00}", Minuites, Seconds);

    }

    }

