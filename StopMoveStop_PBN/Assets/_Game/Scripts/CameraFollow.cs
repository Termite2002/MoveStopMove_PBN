using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    private Transform posPlayer;
    public Vector3 camDistance;

    private Vector3 beginCam;
    private Vector3 zoomIn = new Vector3(0.1f, -4f, 5f);

    private int statusCam = 0;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

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
        beginCam = camDistance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (statusCam == 1)
        {
            ZoomIn();
        }
        if (statusCam == 2)
        {
            ZoomOut();
        }
        TF.position = posPlayer.position - camDistance;
    }
    public void ZoomIn()
    {
        Vector3 curPos = camDistance;
        Vector3 newPos = Vector3.SmoothDamp(curPos, zoomIn, ref velocity, smoothTime);

        camDistance = newPos;
    }
    public void ZoomOut()
    {
        Vector3 curPos = camDistance;
        Vector3 newPos = Vector3.SmoothDamp(curPos, beginCam, ref velocity, smoothTime);

        camDistance = newPos;
    }
    public void StartZoomIn()
    {
        statusCam = 1;
    }
    public void StartZoomOut()
    {
        statusCam = 2;
    }
    public void ResetCam()
    {
        camDistance = beginCam;
    }
}
