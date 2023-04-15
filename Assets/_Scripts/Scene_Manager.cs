using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Manager : MonoBehaviour
{
    [HideInInspector]
    public static Scene_Manager Instance { get { return _instance; } }
   // Implement a singleton pattern
    private static Scene_Manager _instance;

    // format for mathtype: "mathType_numDigits"
    public static string mathType = "addition_1";
    public float highScore = 0;
    const string HIGH_SCORE_KEY = "highScore";

    public float math_time = 100f;
    public float platformer_time = 100f;

    private void Awake()
    {
        manageSingleton();
    }

    public static void add_time(string gameType, float time)
    {
        if (gameType == "math")
        {
            Instance.math_time += time;
        }
        else if (gameType == "platformer")
        {
            Instance.platformer_time += time;
        }
    } 

    public static void subtract_time(string gameType, float time)
    {
        if (gameType == "math")
        {
            Instance.math_time -= time;
        }
        else if (gameType == "platformer")
        {
            Instance.platformer_time -= time;
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
}
