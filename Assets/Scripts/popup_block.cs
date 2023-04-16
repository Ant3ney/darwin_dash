using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup_block : MonoBehaviour
{
    BoxCollider2D boxCollider;
    GameObject poped_up_block;
    
    void Start(){
        boxCollider = GetComponent<BoxCollider2D>();
        poped_up_block = transform.GetChild(0).gameObject;
    }

    void Update(){
        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit_collision in collisions){
            
        	if (hit_collision == boxCollider)  // Ignore collision with self
        	continue;

            // Get the distance between the two colliders
            ColliderDistance2D colliderDistance = hit_collision.Distance(boxCollider);

            if (colliderDistance.isOverlapped && hit_collision.gameObject.tag == "Player")
                poped_up_block.SetActive(true);
        }
    }

     private void OnDrawGizmos() {
        Color gizmoColor = Color.yellow;
        Vector3 size = 2 * Vector3.one;
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, size);
    }
}
