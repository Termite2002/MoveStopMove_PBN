using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] List<BotSpawner> spawnerList = new List<BotSpawner>();

    public List<Character> allAlivePosition = new List<Character>();

    public LevelController lvController;
    int currentSpawn;

    public void OnInitSpawn(LevelController new_lv)
    {
        lvController = new_lv;
        currentSpawn = 0;
        foreach (BotSpawner spawner in spawnerList)
        {
            spawner.SpawnBot(lvController);
        }
    }
    public void Respawn()
    {
        int stop = 0;
        while (spawnerList[currentSpawn].CheckIfHasEnemyInRange())
        {
            stop++;
            currentSpawn++;
            if (currentSpawn == spawnerList.Count)
            {
                currentSpawn = 0;
            }
            if (stop > 10)
            {
                //Debug.Log("damn");
                break;
            }
        }
        if (!spawnerList[currentSpawn].CheckIfHasEnemyInRange())
        {
            spawnerList[currentSpawn].SpawnBot(lvController);
        }
        currentSpawn++;
        if(currentSpawn == spawnerList.Count)
        {
            currentSpawn = 0;
        }
    }
}
