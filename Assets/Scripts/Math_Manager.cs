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
    int incorrect = 3;
    float globaltimer = Scene_Manager.getTime();

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


       submitAnswesbttn();
            
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

        if (userAnswer == "DEV") {
            status_text.text = "DEV!";
            correctSound.Play();
            setStatusActive();
            Scene_Manager.add_time(9999);
            incorrect = 0;
            Scene_Manager.add_highScore(-9999);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (userAnswer == answer.ToString()){
            status_text.text = "Correct!";
            correctSound.Play();
            setStatusActive();
            displayNextProblem();
            Scene_Manager.add_time(15);
            Scene_Manager.add_highScore(100);
        } else {
            status_text.text = "Incorrect!";
            incorrectSound.Play();
            setStatusActive();
            displayNextProblem();
            Scene_Manager.add_time(-15);
            Scene_Manager.add_highScore(-140);
            incorrect--;

            if (incorrect == 0 && globaltimer > 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                  }
            else if (incorrect == 0 && globaltimer == 0) {
                Scene_Manager.add_time(45);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            }
        
    }

    public void submitAnswesbttn() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            submitAnswer();
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
