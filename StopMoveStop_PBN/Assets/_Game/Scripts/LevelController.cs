using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<Character> allAlivePosition = new List<Character>();
    public int allAlive = 99;

    public void OnClearLevel()
    {
        foreach(Character character in allAlivePosition)
        {
            if (!character.isPlayer)
            {
                character.GetComponent<Bot>().RemoveFromFloor();
            }
        }
        allAlivePosition.RemoveAll(Character => !Character.isPlayer);

        allAlive = 99;
    }
    private void Update()
    {
        allAlivePosition.RemoveAll(Character => Character == null);
        allAlivePosition.RemoveAll(Character => Character.GetComponent<Character>().isDead);
    }

}
