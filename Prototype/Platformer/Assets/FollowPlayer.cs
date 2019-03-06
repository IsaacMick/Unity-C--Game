using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private GameObject player;

    //These may look backwards, but for the math in LateUpdate() the values need to be negated. So -3 is actually 3.
    public float minX = 3.5f;
    public float maxX = -2f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	// Update is called once per frame
	void LateUpdate () {
        //Follows the player.
        Vector3 camPosition = gameObject.transform.position;
        Vector3 playerPosition = player.transform.position;

        //The most the player can move to the left without the camera following.
        if ((camPosition.x - minX) > playerPosition.x)
        {
            camPosition.x = playerPosition.x + minX;
        }

        //The most the player can move to the right without the camera following.
        else if (camPosition.x + maxX < playerPosition.x)
        {
            camPosition.x = playerPosition.x - maxX;
        }

        gameObject.transform.position = camPosition;
	}
}
