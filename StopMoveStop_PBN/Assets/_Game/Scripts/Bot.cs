using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;


    private int targetIndex;
    private IState currentState;
    [SerializeField] private GameObject attackRange;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private LevelController lvController;

    public override void Start()
    {
        lvController = FindObjectOfType<LevelController>();
        spawnManager = FindObjectOfType<SpawnManager>();
        agent = GetComponent<NavMeshAgent>();
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        targetListInRange.RemoveAll(Character => Character == null);

        targetListInRange.RemoveAll(Character => Character.GetComponent<Character>().isDead);
        

        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }
    private void OnInit()
    {
        isDead = false;
        ChangeState(new IdleState());
    }
    public void ChangeTarget()
    {
        targetIndex = Random.Range(0, lvController.allAlivePosition.Count);
        while (lvController.allAlivePosition[targetIndex] != null && Vector3.Distance(transform.position, lvController.allAlivePosition[targetIndex].transform.position) < 0.05f)
        {
            targetIndex = Random.Range(0, lvController.allAlivePosition.Count);
        }
    }
    public void Moving()
    {
        agent.isStopped = false;
        ChangeAnim("Run");
        if (targetIndex >= lvController.allAlivePosition.Count)
        {
            ChangeTarget();
        }
        if (lvController.allAlivePosition[targetIndex] != null)
        {
            //Debug.Log(LevelController.Instance.allAlivePosition[targetIndex].transform.position);
            agent.destination = lvController.allAlivePosition[targetIndex].transform.position;
        }
    }
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void StopMoving()
    {
        ChangeAnim("Idle");
        
        agent.isStopped = true;
    }
    public override void Attack(Vector3 targetPosition)
    {
        ChangeAnim("Attack");
        transform.LookAt(targetPosition);
        base.Attack(targetPosition);
    }

    public void DestroyBot()
    {
        targetListInRange.Clear();
        StartCoroutine(Dying());
    }
    public IEnumerator Dying()
    {
        yield return new WaitForSeconds(3f);
        targetListInRange.Clear();
        ObjectPoolPro.Instance.ReturnToPool("Bot", gameObject);
        gameObject.SetActive(false);
        lvController.allAlive--;

        // Respawn
        spawnManager.Respawn();
    }
    public void RemoveFromFloor()
    {
        targetListInRange.Clear();
        ObjectPoolPro.Instance.ReturnToPool("Bot", gameObject);
        gameObject.SetActive(false);
    }
}
