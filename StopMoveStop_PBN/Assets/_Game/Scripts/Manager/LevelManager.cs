using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levelList = new List<Level>();
    public List<NavMeshData> navmeshList = new List<NavMeshData>();
    public Player player;
    public GameObject planeStart;


    Level currentLevel;

    //[SerializeField] private LevelController lvController;
    int level = 1;
    public int coinGainInLevel = 0;

    //public LevelController LvController { get => lvController; set => lvController = value; }
   [field: SerializeField]  public LevelController LvController { get; set; }

    private int resetCheck = 0;
    public int ResetCheck { get => resetCheck; }
    
    //TODO: k can dung update -> toi uu (DONE)


    public void LoadLevel()
    {
        resetCheck++;
        LoadLevel(level);
        OnInit();
    }
    public void LoadLevel(int indexLevel)
    {
        planeStart.SetActive(false);
        if (currentLevel != null)
        {
            LvController.OnClearLevel();
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levelList[indexLevel - 1]);
        //lvController = currentLevel.levelController;
        LvController = FindObjectOfType<LevelController>();
        LvController.allAlivePosition.Add(player);
        SpawnManager.Instance.OnInitSpawn(LvController);
        coinGainInLevel = 0;
        UIManager.Instance.GetUI<UIGameplay>().ChangeAlive(100);
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navmeshList[indexLevel-1]);
    }
    public void DeleteLevel()
    {
        
        if (currentLevel != null)
        {
            LvController.OnClearLevel();
            Destroy(currentLevel.gameObject);
        }
        planeStart.SetActive(true);
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
        if (LvController.allAlive <= 0)
        {
            player.PlayParticle(0);
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
        LvController = FindObjectOfType<LevelController>();
        player.AddHeadPoint();
        // Player co the di chuyen
        SetJoystickOn();
    }
    public void SetJoystickOn()
    {
        player.GetComponent<PlayerController>().enabled = true;
    }
    public void SetJoystickOff()
    {
        player.GetComponent<PlayerController>().enabled = false;
    }
}
