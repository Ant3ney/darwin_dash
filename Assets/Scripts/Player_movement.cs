using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Player_movement : MonoBehaviour
{

    private Camera Camer_to_bound;
    public float Player_speed = 10;
    public float Player_jump_force = 3;
    public float Plyaer_gravity = -1;

    private Rigidbody2D Player;


    private void Awake()
    {
        Player = GetComponent<Rigidbody2D>();
        Camer_to_bound = Camera.main;

    }

    private void Update() { 
        Vector2 position = Player.position;
        Vector2 LeftEdge = Camer_to_bound.ScreenToWorldPoint(Vector2.zero);
        Vector2 RightEdge = Camer_to_bound.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, LeftEdge.x + 1f, RightEdge.x - 1f);

        Player.MovePosition(position);
    }



    void FixedUpdate()
    {
        Horizotal_movement();
        Vertail_movement();

    }


    private void Horizotal_movement()
    {
        float movement = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(movement * Player_speed, Plyaer_gravity);

        Player.AddForce(move * Player_speed);
    }
    private void Vertail_movement()
    {
        float Vmovement = Input.GetAxis("Vertical");
        if (Vmovement > 0)
        {
            Vector2 move = new Vector2(0, Vmovement + Player_jump_force);
            Player.AddForce(move * Player_speed);
        }
    }
}
