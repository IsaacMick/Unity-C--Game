using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int playerSpeed = 500;
    public int playerJump = 40000;
    private float playerX;
    private float playerY;
    private bool isRight = true;
    private bool grounded;
    private int jumpNum;
    public int maxJump = 1;

	// Update is called once per frame
	void FixedUpdate () {
        playerMove();
	}

    void playerMove()
    {
        //Control
        playerX = Input.GetAxis("Horizontal");

        //When either arrow is pushed, the character will move in that direction.
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(playerX * playerSpeed * Time.deltaTime, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        //When the "Jump" button is pushed, the character will jump
        if (Input.GetButtonDown("Jump") && jumpNum < maxJump){
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJump * Time.deltaTime);
            jumpNum += 1;
        }

        //Changes the sprites image depending on the way the character is moving.
        if ((playerX < 0 && isRight == true) || (playerX > 0 && isRight == false))
        {
            flipSprite();
        }

    }

    void flipSprite()
    {
        isRight = !isRight;
        Vector2 direction = gameObject.transform.localScale;
        direction.x = -1 * direction.x;
        transform.localScale = direction;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Debug.Log(collisionInfo.collider.name);
        if(collisionInfo.collider.tag == "Ground_Tag")
        {
            jumpNum = 0;
        }


    }
}
