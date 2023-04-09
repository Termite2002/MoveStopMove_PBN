
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;


    private int targetIndex;
    private IState currentState;
    [SerializeField] private SpawnManager spawnManager;
    public LevelController lvController;


    protected override void Start()
    {
        base.Start();
        lvController = FindObjectOfType<LevelController>();
        spawnManager = FindObjectOfType<SpawnManager>();
        agent = GetComponent<NavMeshAgent>();
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: fix lai  (DONE)
        //targetListInRange.RemoveAll(Character => Character == null);

        //targetListInRange.RemoveAll(Character => Character.GetComponent<Character>().IsDead);
        

        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }
    private void OnInit()
    {
        isDead = false;
        //atkRange.ResetSize();
        ChooseRandomWeaponForBot();
        RenderWeaponToHold();
        RenderHatToWear();
        ChangeState(new IdleState());
    }
    public void OnDespawn()
    {
        IsDead = true;
        ChangeState(new DeadState());
        ChangeAnim(Constant.ANIM_DEATH);
        DestroyBot();
    }
    public void ChangeTarget()
    {
        lvController.UpdateAllAliveList();
        targetIndex = Random.Range(0, lvController.allAlivePosition.Count);
        while (lvController.allAlivePosition[targetIndex] != null && Vector3.Distance(TF.position, lvController.allAlivePosition[targetIndex].TF.position) < 0.05f)
        {
            targetIndex = Random.Range(0, lvController.allAlivePosition.Count);
        }
    }
    public void Moving()
    {
        agent.isStopped = false;
        //cache string  (DONE)
        ChangeAnim(Constant.ANIM_RUN);
        lvController.UpdateAllAliveList();
        if (targetIndex >= lvController.allAlivePosition.Count)
        {
            ChangeTarget();
        }
        if (lvController.allAlivePosition[targetIndex] != null)
        {
            //Debug.Log(LevelController.Instance.allAlivePosition[targetIndex].transform.position);
            agent.destination = lvController.allAlivePosition[targetIndex].TF.position;
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
        ChangeAnim(Constant.ANIM_IDLE);
        
        agent.isStopped = true;
    }
    public override void Attack(Vector3 targetPosition)
    {
        ChangeAnim(Constant.ANIM_ATTACK);
        TF.LookAt(targetPosition);
        base.Attack(targetPosition);
    }

    public void DestroyBot()
    {
        targetListInRange.Clear();
        StartCoroutine(IEDying());
    }
    public IEnumerator IEDying()
    {
        yield return new WaitForSeconds(2.5f);
        //targetListInRange.Clear();
        ObjectPoolPro.Instance.ReturnToPool(Constant.GAME_BOT, gameObject);
        gameObject.SetActive(false);

        // Respawn
        if (lvController != null)
        {
            if (lvController.allAlive > 10)
            {
                spawnManager.Respawn();
            }
            lvController.allAlive--;
            //MyUIManager.Instance.SetAlive(lvController.allAlive);
            LevelManager.Instance.CheckIfPlayerWin();
        }
    }
    public void RemoveFromFloor()
    {
        targetListInRange.Clear();
        ObjectPoolPro.Instance.ReturnToPool(Constant.GAME_BOT, gameObject);
        gameObject.SetActive(false);
    }
    public void ChooseRandomWeaponForBot()
    {
        ArrayList values = new ArrayList(System.Enum.GetValues(typeof(WeaponType)));
        int randomIndex = Random.Range(0, values.Count);
        WeaponType randomEnum = (WeaponType)System.Enum.ToObject(typeof(WeaponType), (int)values[randomIndex]);
        currentWeapon = randomEnum;
    }
    public void RenderHatToWear()
    {
        GameObject hat = ObjectPoolPro.Instance.GetFromPool("HeadArrow");
        hat.transform.position = headPoint.position;
        hat.SetActive(true);
        hat.transform.SetParent(headPoint);
    }
}
