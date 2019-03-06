using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour {
    GameObject player;
    public float yUnitsUp = 2;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 objectPosition = gameObject.transform.position;
        Vector3 playerPosition = player.transform.position;

        objectPosition = playerPosition;

        objectPosition.y += yUnitsUp;

        gameObject.transform.position = objectPosition;
	}
}
