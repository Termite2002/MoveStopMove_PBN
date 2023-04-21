using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<Character> allAlivePosition = new List<Character>();
    public int allAlive = 100;

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

        allAlive = 100;
    }

    // Ham chua su dung
    public void StopAllBot()
    {
        foreach (Character character in allAlivePosition)
        {
            if (!character.isPlayer)
            {
                character.GetComponent<Bot>().agent.isStopped = true;
            }
        }
    }
    
    //private void Update()
    //{

    //    allAlivePosition.RemoveAll(Character => Character == null);
    //    allAlivePosition.RemoveAll(Character => Character.IsDead);
    //}

    public void UpdateAllAliveList()
    {
        allAlivePosition.RemoveAll(Character => Character == null);
        allAlivePosition.RemoveAll(Character => Character.IsDead);
    }
   
}
