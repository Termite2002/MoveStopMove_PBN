using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private NavMeshAgent agent;
    private int targetIndex;
    public override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating(nameof(ChangeTarget), 0f, 3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelController.Instance.allAlivePosition[targetIndex] != null)
        {
            agent.destination = LevelController.Instance.allAlivePosition[targetIndex].transform.position;
        }
    }
    private void ChangeTarget()
    {
        targetIndex = Random.Range(0, LevelController.Instance.allAlivePosition.Count);
    }
}
