using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Math_Manager : MonoBehaviour
{
    public int max_status_display = 2;
    public float status_display_timer;

    public AudioSource correctSound;
    public AudioSource incorrectSound;
    bool status_displayed;
    string mathTypeCache;
    Text answerInputFelid;
    Text mathProblemText;
    int question1;
    int question2;
    int answer;
    int incorrect = 1;
    float timer = Scene_Manager.getTime();

    string userAnswer;
    GameObject status_container;
    Text status_text;
    // Start is called before the first frame update
    void Start()
    {
        //TODO: get mathType from Scene_Manager via method call
        mathTypeCache = Scene_Manager.mathType;

        answerInputFelid = GameObject.Find("user_input_feild").GetComponent<Text>();
        status_container.SetActive(false);
        status_displayed = false;
    }
    void Update()
    {
        
    }

    public void submitAnswer()
    {
        userAnswer = answerInputFelid.text;
        Debug.Log("textbox is:" + userAnswer + " and answer.ToString() is: " + answer.ToString());
        
        }

 

   

   
}
