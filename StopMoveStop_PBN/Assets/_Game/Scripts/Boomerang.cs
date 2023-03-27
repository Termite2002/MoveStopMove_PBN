using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon
{
    private float timer;
    private Vector3 beginPos;
    private float returnSpeed = 10;

    protected override void OnEnable()
    {
        base.OnEnable();
        TF.localEulerAngles = new Vector3(90f, 0, 0);
        beginPos = TF.position;
        timer = 0;
    }
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        type = WeaponType.Boomerang;
        timer = 0;
    }
    void FixedUpdate()
    {
        //transform.Rotate(Vector3.down, rotareSpeed);
        tf.localEulerAngles += new Vector3(0, 0, 10);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            rb.velocity = Vector3.zero;
            //TODO: cache transform (DONE)
            tf.position = Vector3.MoveTowards(TF.position, beginPos, returnSpeed*Time.deltaTime);
            if (Vector3.Distance(TF.position, beginPos) < 0.2f)
            {
                DestroyWeapon();
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
