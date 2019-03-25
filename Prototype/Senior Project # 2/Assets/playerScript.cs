using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int Health = 100;
    }

    public PlayerStats playerStats = new PlayerStats();

    public int fallBoundary = -30;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -30)
        {
            DamagePlayer(99999999);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStats.Health -= damage;
        if (playerStats.Health <= fallBoundary)
        {
            GameMasterScript.killPlayer(this);
        }
    }
}
