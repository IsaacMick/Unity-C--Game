using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour {
   public static GameMasterScript gm;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMasterScript>();
        }
    }

    public IEnumerator respondPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Add spawn particles");
    }

   
    public static void killPlayer(playerScript player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.respondPlayer());
    }
}
