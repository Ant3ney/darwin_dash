using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class Send_High_Score : MonoBehaviour
{
    void Awake(){
        /* send_The_High_Score("ANT", 100000); */
    }
    /* public delegate void highScoreSent(string status);
    public static event highScoreSent onHighScoreSent; */
    void Start()
    {
        
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
                /* if(onHighScoreSent  != null) onHighScoreSent("success"); */
            }
            else
            {
                Debug.Log("POST failed. Error: " + webRequest.error);
                /*  if(onHighScoreSent != null) onHighScoreSent("fail"); */
            }
        }
    }
}
