using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour {
   public static GameMasterScript gm;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 3.5f;
    public Transform spawnPrefab;
    private float startOfParticles;
    public float timeToDeleteParticles = 2.5f;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMasterScript>();
        }
    }

    public IEnumerator respondPlayer()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clonePart = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation).gameObject;

        startOfParticles = Time.deltaTime;

        Destroy(clonePart, timeToDeleteParticles);
    }

   
    public static void killPlayer(playerScript player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.respondPlayer());
    }

    public static void killEnemy(Enemy enemy)
    {
        gm.KillEnemy(enemy);
    }

    public void KillEnemy(Enemy enemy)
    {
        GameObject enemyParticles = Instantiate(enemy.deathParticles, enemy.transform.position, Quaternion.identity).gameObject;
        Destroy(enemy.gameObject);
        Destroy(enemyParticles, 2);
    }
}
