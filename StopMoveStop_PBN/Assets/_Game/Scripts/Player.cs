using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Vector3 FindNearestBotInRange()
    {
        Vector3 nearestBotPotition = Vector3.zero;
        float closestDistance = Mathf.Infinity;
        Character removeCharacter = new Character();

        foreach (Character character in targetListInRange)
        {
            float distance = Vector3.Distance(character.GetComponent<Character>().transform.position, transform.position);

            if (distance < closestDistance)
            {
                removeCharacter = character;
                closestDistance = distance;
                nearestBotPotition = character.GetComponent<Character>().transform.position;
            }
        }
        targetListInRange.Remove(removeCharacter);
        return nearestBotPotition;
    }
}
