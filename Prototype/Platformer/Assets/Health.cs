using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    public int health;

    void Start()
    {
        health = 100;
    }
	// Update is called once per frame
	void Update ()
    {
		if (health <=0)
        {
            SceneManager.LoadScene("Prototype level_001");
        }
	}

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "InstantDeath")
        {
            health = 0;
        }
    }
}
