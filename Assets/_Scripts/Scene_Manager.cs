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

    private void Awake()
    {
        manageSingleton();
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
    
    public static void loadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
