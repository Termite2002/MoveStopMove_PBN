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
    private void Awake()
    {
        LoadLevel(1);
        lvController = FindObjectOfType<LevelController>();
    }

    void Update()
    {
        if (lvController.allAlive <= 1)
        {
            UIManager.Instance.OpenWinUI();

            SoundManager.Instance.PlaySFX(4);
            //lvController.StopAllBot();
        }
        if (player.isDead)
        {
            UIManager.Instance.OpenLoseUI();

            SoundManager.Instance.PlaySFX(0);
        }
    }

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
        lvController = FindObjectOfType<LevelController>();
        lvController.allAlivePosition.Add(player);
        SpawnManager.Instance.OnInitSpawn(lvController);
        UIManager.Instance.SetAlive(lvController.allAlive);
    }
    public void OnInit()
    {
        player.transform.position = currentLevel.startPoint.transform.position;
        player.OnInit();
        player.isDead = false;
        player.ChangeAnim("Idle");
    }
    public void NextLevel()
    {
        level++;

        LoadLevel();
    }
}
