using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Math_Manager : MonoBehaviour
{
    public int max_status_display = 2;
    public float status_display_timer;
    bool status_displayed;
    string mathTypeCache;
    Text answerInputFelid;
    Text mathProblemText;
    int question1;
    int question2;
    int answer;

    string userAnswer;
    GameObject status_container;
    Text status_text;
    // Start is called before the first frame update
    void Start(){
        //TODO: get mathType from Scene_Manager via method call
        mathTypeCache = Scene_Manager.mathType;

        answerInputFelid = GameObject.Find("user_input_feild").GetComponent<Text>();
        mathProblemText = GameObject.Find("math_problem").GetComponent<Text>();
        status_container = GameObject.Find("status_section");
        status_text = GameObject.Find("status_text").GetComponent<Text>();

        status_container.SetActive(false);
        status_displayed = false;

        displayNextProblem();
    }

    void Update(){
        if (status_displayed){
            status_display_timer += Time.deltaTime;
            if (status_display_timer >= max_status_display){
                status_container.SetActive(false);
                status_displayed = false;
                status_display_timer = 0;
            }
        }
    }

    public void submitAnswer(){
        userAnswer = answerInputFelid.text;
        Debug.Log("textbox is:" + userAnswer + " and answer.ToString() is: " + answer.ToString());

        if (userAnswer == answer.ToString()){
            status_text.text = "Correct!";
            setStatusActive();
            displayNextProblem();
            Scene_Manager.add_time(1510);
        } else {
            status_text.text = "Incorrect!";
            setStatusActive();
            displayNextProblem();
            Scene_Manager.add_time(-15);
        }
    }

    public void setStatusActive(){
         status_container.SetActive(true);
            status_displayed = true;
    }

    public void displayNextProblem(){
        question1 = (int)Mathf.Round(Random.Range(0, 10));
        question2 = (int)Mathf.Round(Random.Range(0, 10));
        answer = question1 + question2;

        mathProblemText.text = question1 + " + " + question2 + " = ?";
    }
}