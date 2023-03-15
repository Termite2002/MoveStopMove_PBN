using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform posPlayer;
    private Vector3 camDistance;
    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        posPlayer = temp.GetComponent<Transform>();
        camDistance = posPlayer.transform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = posPlayer.transform.position - camDistance;
    }
}
