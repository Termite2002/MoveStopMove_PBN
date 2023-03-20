using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.rotation = Quaternion.Euler(180f, 0f, 0f);  //new Vector3(267f, -26f, 20f);
        Invoke(nameof(DestroyWeapon), 3f);
    }
    protected override void OnDisable()
    {
        CancelInvoke(nameof(DestroyWeapon));
        base.OnDisable();
        transform.rotation = Quaternion.Euler(180f, 0f, 0f);  //new Vector3(267f, -26f, 20f);
    }

    protected override void Start()
    {
        base.Start();
        type = WeaponType.Axe;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
