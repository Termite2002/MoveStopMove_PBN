using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public bool hasPlayer;
    [SerializeField] private LevelController lvController;

    private void Start()
    {
        lvController = FindObjectOfType<LevelController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Character>() is Character)
        {
            hasPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>() is Character)
        {
            hasPlayer = false;
        }
    }
    public void SpawnBot()
    {
        GameObject bot = ObjectPoolPro.Instance.GetFromPool("Bot");
        bot.transform.position = transform.position;
        bot.SetActive(true);
        bot.GetComponent<Bot>().isDead = false;
        lvController.allAlivePosition.Add(bot.GetComponent<Bot>());
    }
}
