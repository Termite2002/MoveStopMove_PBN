using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<BotSpawner> spawnerList = new List<BotSpawner>();
    public int playerOnGround = 0;
    //int playerMax = 10;
    int currentSpawn;
    void Start()
    {
        currentSpawn = 0;
        foreach(BotSpawner spawner in spawnerList)
        {
            spawner.SpawnBot();
            playerOnGround++;
        }
    }

    public void Respawn()
    {
        if (!spawnerList[currentSpawn].hasPlayer)
        {
            spawnerList[currentSpawn].SpawnBot();
        }
        currentSpawn++;
        if(currentSpawn == spawnerList.Count)
        {
            currentSpawn = 0;
        }
    }
}
