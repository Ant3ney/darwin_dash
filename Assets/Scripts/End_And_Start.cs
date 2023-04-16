using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_And_Start : MonoBehaviour
{
   
    public void toHome() {
        SceneManager.LoadScene(1);
    }

    public void end() {
        Application.Quit();
    }
}
