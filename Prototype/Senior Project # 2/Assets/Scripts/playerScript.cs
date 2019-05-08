using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int curHealth;
        public int currentHealth
        {
            get
            {
                return curHealth;
            }
            set
            {
                curHealth = Mathf.Clamp(value, 0, maxHealth);
            }
        }
    }

    public PlayerStats playerStats = new PlayerStats();

    [SerializeField]
    private HealthCanvasScript statusIndicator;

    public int fallBoundary = -30;

    private void Start()
    {
        playerStats.currentHealth = playerStats.maxHealth;

        if(statusIndicator == null)
        {
            Debug.LogError("NO HEALTH CANVAS ON PLAYER");
        }

        else
        {
            statusIndicator.SetHealth(playerStats.maxHealth, playerStats.currentHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= fallBoundary)
        {
            DamagePlayer(99999999);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStats.currentHealth -= damage;
        if (playerStats.currentHealth <= 0)
        {
            GameMasterScript.killPlayer(this);
        }

        statusIndicator.SetHealth(playerStats.maxHealth, playerStats.currentHealth);
    }
}
