using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float enemySpeed = 2f;
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed * Time.deltaTime, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Box_Tag")
        {
            flip();
        }
    }

    void flip()
    {
        enemySpeed *= -1;
    }
}
