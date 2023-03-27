using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public bool onAttack = false;
    public Vector3 throwDirection;
    protected override void OnEnable()
    {
        base.OnEnable();
        //TF.rotation = Quaternion.Euler(90f, 0f, 0f);  //new Vector3(267f, -26f, 20f);
        Invoke(nameof(DestroyWeapon), 3f);
    }
    protected override void OnDisable()
    {
        CancelInvoke(nameof(DestroyWeapon));
        base.OnDisable();
        //TF.rotation = Quaternion.Euler(90f, 0f, 0f);  //new Vector3(267f, -26f, 20f);
    }

    protected override void Start()
    {
        base.Start();
        type = WeaponType.Sword;
    }
    private void Update()
    {
        //if (onAttack)
        //{
        //    TF.rotation = Quaternion.Euler(90f, throwDirection.y, 0f);
        //}
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
