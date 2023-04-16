using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Send_High_Score : MonoBehaviour
{
    public delegate void highScoreSent(string status);
    public static event highScoreSent onHighScoreSent;
    bool showPostStatus = false;
    public float maxStatusTimer = 2f;
    float statusTimer = 0f;
    Text answerInputFelid;

    public GameObject Status_Container_Obj;
    public Text status_text;
    int totalnum; 

    void Start()
    {
        if(GameObject.Find("user_input_feild") )
            answerInputFelid = GameObject.Find("user_input_feild").GetComponent<Text>();

        if(Status_Container_Obj)
        {
            Status_Container_Obj.SetActive(false);
        }
    }
    void Awake(){
        /* send_The_High_Score("ANT", 100000); */
    }
    
    /* public delegate void highScoreSent(string status);
    public static event highScoreSent onHighScoreSent; */


    public  void submitAnswer()
    {
        if (answerInputFelid.text.Length == 3 && Scene_Manager.submitted_check())
        {
            send_The_High_Score(answerInputFelid.text, Scene_Manager.get_highscore());
            Debug.Log("submitting answer" + " " + answerInputFelid.text + " " + Scene_Manager.get_highscore());
            Scene_Manager.submitted_add();
        }
        else
        {
            Status_Container_Obj.SetActive(true);
            status_text.text = "You entered an invalid Initial";
            Debug.Log("NOOOOOOOOOOOOOOOOOr" + " " + answerInputFelid.text + " " + Scene_Manager.get_highscore());
        }

    }


    public void Update() {
        if (Input.anyKeyDown)
        {
            totalnum++;
        }   

        if (showPostStatus)
        {
            Debug.Log("showing post status");
            statusTimer += Time.deltaTime;
            if (statusTimer >= maxStatusTimer)
            {
                Status_Container_Obj.SetActive(false);
                showPostStatus = false;
                statusTimer = 0;
                SceneManager.LoadScene(0);
            }
        }
    }





    public void send_The_High_Score(string userInitials, float score)
    {
        StartCoroutine(PostRequest("https://darwin-dash.herokuapp.com/score", "{\"userInitials\":\"" + userInitials +"\", \"userScore\":\"" + score + "\"}"));
    }

    IEnumerator PostRequest(string url, string jsonData)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, "POST"))
        {
            Debug.Log("POSTing to: " + url);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                
                Debug.Log("POST successful!");
                Debug.Log("Response: " + webRequest.downloadHandler.text);
                

                    Status_Container_Obj.SetActive(true);
                    status_text.text = "High Score Submitted Successfully!";
                    showPostStatus = true;
                
            }
            else
            {
                
                Debug.Log("POST failed. Error: " + webRequest.error);
                  Status_Container_Obj.SetActive(true);
                    status_text.text = "High Score Failed to Submitted!";
                    showPostStatus = true;
                
            }
        }
    }}
