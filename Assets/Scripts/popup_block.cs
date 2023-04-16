using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup_block : MonoBehaviour
{
    
    void Update(){
        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits){
            
        	if (hit == boxCollider)  // Ignore collision with self
        	continue;

            // Get the distance between the two colliders
            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped && hit.gameObject.tag == "Bullet")
                bulletInMe(hit.gameObject.GetComponent<Bullet>());
        }
    }
}
