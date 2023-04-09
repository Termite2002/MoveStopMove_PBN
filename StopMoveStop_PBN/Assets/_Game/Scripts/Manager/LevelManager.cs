using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levelList = new List<Level>();
    public Player player;



    Level currentLevel;

    [SerializeField] private LevelController lvController;

    int level = 1;
    public int coinGainInLevel = 0;
    //TODO: k can dung update -> toi uu (DONE)


    public void LoadLevel()
    {
        LoadLevel(level);
        OnInit();
    }
    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null)
        {
            lvController.OnClearLevel();
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levelList[indexLevel - 1]);
        //lvController = currentLevel.levelController;
        lvController = FindObjectOfType<LevelController>();
        lvController.allAlivePosition.Add(player);
        SpawnManager.Instance.OnInitSpawn(lvController);
        coinGainInLevel = 0;
        //MyUIManager.Instance.SetAlive(lvController.allAlive);
    }
    public void DeleteLevel()
    {
        if (currentLevel != null)
        {
            lvController.OnClearLevel();
            Destroy(currentLevel.gameObject);
        }

        player.OnInit();
        player.ChangeAnim(Constant.ANIM_IDLE);
    }
    public void OnInit()
    {
        //TODO: cache transform (DONE)
        player.TF.position = currentLevel.startPoint.position;
        player.OnInit();
        //TODO: cache string (DONE)
        player.ChangeAnim(Constant.ANIM_IDLE);
    }
    public void NextLevel()
    {
        level++;

        LoadLevel();
    }
    public void CheckIfPlayerWin()
    {
        if (lvController.allAlive <= 1)
        {
            //MyUIManager.Instance.OpenWinUI();
            UIManager.Instance.OpenUI<UIWin>();
            SaveLoadController.Instance.SaveData(player);

            SoundManager.Instance.PlaySound(4);
            //lvController.StopAllBot();
        }
    }
    public void WhenPlayerLose()
    {
        if (player.IsDead)
        {
            UIManager.Instance.OpenUI<UILose>();
            SaveLoadController.Instance.SaveData(player);

            SoundManager.Instance.PlaySound(0);
        }
    }
    public void OnStart()
    {
        LoadLevel(1);
        lvController = FindObjectOfType<LevelController>();
        player.GetComponent<PlayerController>().enabled = true;
    }
}
