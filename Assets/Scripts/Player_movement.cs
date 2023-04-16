using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class Player_movement : MonoBehaviour
{

    SpriteRenderer playerPreview;
    private Camera Camer_to_bound;
    public float rb_jump_force = 3;
    public float Plyaer_gravity = -1;
    private Vector3 respawnPoint;
    public GameObject FallDetector;

    public Animator animator;

    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    bool facingRight = true;

    [Header("Vertail Movement")]
    public float jumpForce = 8f;
    float jumpTimer; 
    public float jumpDelay = 0.25f;

    [Header("Components")]
    public Rigidbody2D rb;
    public LayerMask groundLayer;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1;
    public float fallMultiplier = 5f;

    [Header("Collection")]
    public bool onGround = false;
    public float groundLength = 1f;
    public Vector3 colliderOffset;

    void Start() {
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        playerPreview = GetComponent<SpriteRenderer>();
        playerPreview.enabled = false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Camer_to_bound = Camera.main;
    }

    private void Update() { 
        /* The old way of moving the rb
            Vector2 position = rb.position;
            Vector2 LeftEdge = Camer_to_bound.ScreenToWorldPoint(Vector2.zero);
            Vector2 RightEdge = Camer_to_bound.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            position.x = Mathf.Clamp(position.x, LeftEdge.x + 1f, RightEdge.x - 1f);

            rb.MovePosition(position); 
        */

        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if(Input.GetButtonDown("Jump")){
            jumpTimer = Time.time + jumpDelay;
            
        }

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void moveCharacter(float horizontal){
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        
        if((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
            Flip();

        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
    }



    void FixedUpdate()
    {
        
         moveCharacter(direction.x);
       
        if(jumpTimer > Time.time && onGround) Jump();

        animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("vertical", rb.velocity.y);
        modifyPhysics();
    }


    private void Horizotal_movement()
    {
        float movement = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(movement * moveSpeed, Plyaer_gravity);

        rb.AddForce(move * moveSpeed);
    }
    private void Vertail_movement()
    {
        float Vmovement = Input.GetAxis("Vertical");
        if (Vmovement > 0)
        {
            Vector2 move = new Vector2(0, Vmovement + rb_jump_force);
            rb.AddForce(move * moveSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "KillZone") {
            transform.position = respawnPoint;
            Scene_Manager.add_time(-15);
            Scene_Manager.add_highScore(-140);
        }

        if(collision.tag == "Finsih_line")
        {
            int randomInt = Random.Range(2, SceneManager.sceneCountInBuildSettings - 2);
            Scene_Manager.add_highScore(50 * SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(randomInt);
        }
    }
    void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0;
    }
    void modifyPhysics(){
        bool changingDirection = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
        
        if(onGround){
            if(Mathf.Abs(direction.x) < 0.4f || changingDirection)
                rb.drag = linearDrag;
            else
                rb.drag = 0;

            rb.gravityScale = 0;
        } else{
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if(rb.velocity.y < 0)
                rb.gravityScale = gravity * fallMultiplier;
            else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
                rb.gravityScale = gravity * (fallMultiplier / 2);
        }
    }
    void Flip(){
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }
}
