using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    public Sprite brokenSprite;
    
    public void Die()
    {
        // make the snowman not an obstacle
        gameObject.layer = 0;
        
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());

        GetComponent<SpriteRenderer>().sprite = brokenSprite;
        
        // rescan A* grid
        FindObjectOfType<AstarPath>().Scan();
    }
}
