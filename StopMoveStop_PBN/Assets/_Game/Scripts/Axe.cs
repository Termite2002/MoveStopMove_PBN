using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.localEulerAngles = new Vector3(0f, 90f, 0f);
        Invoke(nameof(DestroyWeapon), 3f);    
    }
    protected override void OnDisable()
    {     
        CancelInvoke(nameof(DestroyWeapon));
        base.OnDisable();
        transform.localEulerAngles = new Vector3(0f, 90f, 0f);
    }

    private void Start()
    {
        type = WeaponType.Axe;
    }
    void FixedUpdate()
    {
        transform.localEulerAngles += new Vector3(0, 0, -10);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
