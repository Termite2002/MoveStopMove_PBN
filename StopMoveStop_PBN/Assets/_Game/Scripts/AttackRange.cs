using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Character player;
    public float radius;

    private Vector3 beginSize;
    [SerializeField] private LineRenderer lineRenderer;

    protected Transform tf;
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
    private void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
        beginSize = TF.localScale;
    }
    //TODO: cache getcomponent (DONE)
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (Cache.GetCharacter(other) is Character)
        {
            player.targetListInRange.Add(Cache.GetCharacter(other));
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (Cache.GetCharacter(other) is Character)
        {
            player.targetListInRange.Remove(Cache.GetCharacter(other));
        }
    }

    public void ResetSize()
    {
        TF.localScale = beginSize;
    }
}
