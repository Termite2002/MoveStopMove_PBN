using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    private Transform posPlayer;
    public Vector3 camDistance;
    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        posPlayer = temp.GetComponent<Transform>();
        camDistance = posPlayer.transform.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = posPlayer.transform.position - camDistance;
    }
}
