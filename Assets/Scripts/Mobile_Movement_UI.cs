using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_Movement_UI : MonoBehaviour
{
    // Start is called before the first frame update
    public Player_movement playerMovement;
    void Start()
    {
        playerMovement = GameObject.Find("player").GetComponent<Player_movement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void clean()
    {
        if (true/* !playerMovement */)
        {
            Start();
        }
    }
    public void pressRight()
    {
        clean();
        Debug.Log("Pressed Right");
        playerMovement.pressingScreenRight = true;
        playerMovement.pressingScreenLeft = false;
    }

    public void pressLeft()
    {
        clean();
        playerMovement.pressingScreenLeft = true;
        playerMovement.pressingScreenRight = false;
    }

    public void releaseRight()
    {

        clean();
        playerMovement.pressingScreenRight = false;
    }

    public void releaseLeft()
    {
        clean();
        playerMovement.pressingScreenLeft = false;
    }

    public void pressJump()
    {
        clean();
        playerMovement.pressingScreenJump = true;
    }

    public void releaseJump()
    {
        clean();
        playerMovement.pressingScreenJump = false;
    }
}
