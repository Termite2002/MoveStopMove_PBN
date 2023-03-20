using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Character player;
    public float radius; 
    [SerializeField] private LineRenderer lineRenderer; 

    void Update()
    {
        // Draw circle
        //Vector3[] positions = new Vector3[500];
        //for (int i = 0; i < positions.Length; i++)
        //{
        //    float angle = i * Mathf.PI * 2 / positions.Length;
        //    Vector3 position = transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        //    positions[i] = position;
        //}

        
        //lineRenderer.positionCount = positions.Length;
        //lineRenderer.SetPositions(positions);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() is Character)
        {
            player.targetListInRange.Add(other.GetComponent<Character>());
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>() is Character)
        {
            player.targetListInRange.Remove(other.GetComponent<Character>());
        }
    }
}
