using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : Singleton<ChangeWeapon>
{
    private float[] rangeList = {5, 4, 5, 4.5f, 4 , 4.5f, 5.5f};
    public void ChangeRangeWhenChangeWeapon(WeaponType type, Character character)
    {
        float range = rangeList[(int)type];
        character.atkRange.TF.localScale = new Vector3(range, range, range);
    }
}
