using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform startPoint;
    [SerializeField] private LevelController lvController;

    Player player; 
    void Start()
    {
        lvController = FindObjectOfType<LevelController>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lvController.allAlive == 1)
        {
            //win
        }
        if (player.isDead)
        {
            //lose
        }
    }
}
