using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public SphereCollider sCollider;
    [SerializeField] private LevelController lvController;
    [SerializeField] private float radius;

    protected Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    private void Start()
    {
        lvController = FindObjectOfType<LevelController>();
        sCollider = GetComponent<SphereCollider>();
        radius = 10.375f;
    }

    //TODO: han che (DONE)
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.GetComponent<Character>() is Character)
    //    {
    //        hasPlayer = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<Character>() is Character)
    //    {
    //        hasPlayer = false;
    //    }
    //}

    public bool CheckIfHasEnemyInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(tf.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(Constant.GAME_BOT) || collider.CompareTag(Constant.GAME_PLAYER))
            {
                return true;
            }
        }
        return false;
    }

    public void SpawnBot(LevelController new_lv)
    {
        lvController = new_lv;
        GameObject bot = ObjectPoolPro.Instance.GetFromPool("Bot");
        bot.transform.position = TF.position;
        bot.SetActive(true);
        bot.GetComponent<Bot>().IsDead = false;
        bot.GetComponent<Bot>().lvController = new_lv;
        lvController.allAlivePosition.Add(bot.GetComponent<Bot>());
    }
}
