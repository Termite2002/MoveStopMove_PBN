using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    private Transform posPlayer;
    public Vector3 camDistance;

    private Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        posPlayer = temp.GetComponent<Transform>();
        camDistance = posPlayer.position - TF.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TF.position = posPlayer.position - camDistance;
    }
}
