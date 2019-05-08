using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform deathParticles;

    [System.Serializable]
    public class EnemyStats
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
                curHealth = (int)Mathf.Clamp(value, 0f, (float) maxHealth);
            }
        }

        public int enemyDamage = 10;
    }
    //public EnemyStats stats = new EnemyStats();

    [SerializeField]
    private HealthCanvasScript statusIndicator;

    public EnemyStats enemyStats = new EnemyStats();
    public void Start()
    {
        enemyStats.currentHealth = enemyStats.maxHealth;

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.maxHealth, enemyStats.currentHealth);
        }

        if(deathParticles == null)
        {
            Debug.LogError("Death Particles are missing from enemy.");
        }
    }

    public void DamageEnemy(int damage)
    {
        enemyStats.currentHealth -= damage;
        if (enemyStats.currentHealth <= 0)
        {
            GameMasterScript.killEnemy(this);
        }

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.maxHealth, enemyStats.currentHealth);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerScript player = collision.collider.GetComponent<playerScript>();
        if (player != null)
        {
            player.DamagePlayer(enemyStats.enemyDamage);
            DamageEnemy(9999999);

        }
    }
}
