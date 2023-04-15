using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float Time_to_set = 60f;
    public Text timeText;

    void Update() {
        if (Time_to_set => 0)
        {
            Time_to_set -= Time.deltaTime;

        }
        else { Time_to_set = 60;
        }

        DisplayTime(Time_to_set);

    }

    void DisplayTime(float time_edit) {
        if (time_edit < 0) { 
            time_edit = 0;
        }

        float Minuites = Mathf.FloorToInt(time_edit / 60);
        float Seconds = Mathf.FloorToInt(time_edit % 60);

        timeText.text = string.Format("{0:00}:{1:00}", Minuites, Seconds);

    }

}
