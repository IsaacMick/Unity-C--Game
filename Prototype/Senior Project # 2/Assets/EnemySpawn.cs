using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class WaveSpawn
    {
        public float spawnRate = 1;
        public int enemiesAtOnce = 1;
    }

    public Transform enemy;
    public WaveSpawn[] waves;
    WaveSpawn wave = new WaveSpawn();
    public float waveCountDown = 5;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (waveCountDown <= 0)
        {
            StartCoroutine(spawnEnemy());
            waveCountDown = 1 / wave.spawnRate;
        }

        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    IEnumerator spawnEnemy()
    {
        if (player == null)
        {
            searchForPlayer();
        }

        else{
            Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z);
            GameObject enemyClone = Instantiate(enemy, newPos, Quaternion.identity).gameObject;
            yield return new WaitForSeconds(1 / wave.spawnRate);
        }
    }

    IEnumerator searchForPlayer()
    {
        GameObject objectSearch = GameObject.FindGameObjectWithTag("Player");
        if (objectSearch == null)
        {
            yield return new WaitForSeconds(.5f);
            StartCoroutine(searchForPlayer());
        }

        else
        {
            player = objectSearch;
            yield return false;
        }

    }
}
