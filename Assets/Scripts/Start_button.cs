using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CoreModule;
using UnityEngine.SceneManagement;

public class Start_button : MonoBehaviour
{

public void Start_Game(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}


}
