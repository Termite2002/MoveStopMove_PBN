using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : Singleton<ChangeWeapon>
{
    public void ChangeRangeWhenChangeWeapon(WeaponType type, Character character)
    {
        int range = (int)type;
        character.atkRange.transform.localScale = new Vector3(range, range, range);
    }
}
