
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    public LevelController lvController;
    public int checkLoad = 0;

    public HeadPoint levelHeadPoint;
    public HeadPoint headpointPrefab;

    private Target targetIndicator;
    private int targetIndex;
    private IState currentState;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private SkinnedMeshRenderer skin;
    [SerializeField] private SkinnedMeshRenderer pantSkin;
    [SerializeField] private List<Material> skinBot = new List<Material>();
    [SerializeField] private List<Material> pantSkinBot = new List<Material>();
    Constant constant = new Constant();
    [SerializeField] private Transform shieldPoint;

    protected override void Start()
    {
        base.Start();
        lvController = FindObjectOfType<LevelController>();
        targetIndicator = GetComponent<Target>();
        spawnManager = FindObjectOfType<SpawnManager>();
        agent = GetComponent<NavMeshAgent>();
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: fix lai  (DONE)
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }
    public void OnInit()
    {
        bodyScale.localScale = Vector3.one;
        enemyKilled = 0;
        isDead = false;
        //AddHeadPoint();
        //atkRange.ResetSize();
        ChooseRandomWeaponForBot();
        RenderWeaponToHold();
        RandomSkinForBot();
        RenderHatToWear();
        RenderShieldToHold();
        ChangeState(new IdleState());
    }
    public void OnNewLevel()
    {
        bodyScale.localScale = Vector3.one;
        enemyKilled = 0;
        isDead = false;
    }
    public void OnDespawn()
    {
        if (levelHeadPoint != null)
        {
            Destroy(levelHeadPoint.gameObject);
        }
        
        //ObjectPoolPro.Instance.ReturnToPool(Constant.HEADPOINT, levelHeadPoint);
        //levelHeadPoint.SetActive(false);
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
        agent.speed = 8;
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
        agent.speed = 0;
        agent.isStopped = true;
        agent.destination = TF.position;
        ChangeAnim(Constant.ANIM_IDLE);
        
        
    }
    public override void Attack(Vector3 targetPosition)
    {
        //ChangeAnim(Constant.ANIM_ATTACK);
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
            UIManager.Instance.GetUI<UIGameplay>().ChangeAlive(lvController.allAlive);
            //MyUIManager.Instance.SetAlive(lvController.allAlive);
            LevelManager.Instance.CheckIfPlayerWin();
        }
    }
    public void RemoveFromFloor()
    {
        if (levelHeadPoint != null)
        {
            Destroy(levelHeadPoint.gameObject);
        }
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
    public void RandomSkinForBot()
    {
        int skinSelected = Random.Range(0, skinBot.Count);
        skin.material = skinBot[skinSelected];

        ChangeColorIndicator();

        int pantSkinSelected = Random.Range(0, pantSkinBot.Count);
        pantSkin.material = pantSkinBot[pantSkinSelected];
    }
    public void RenderHatToWear()
    {
        int hatSelected = Random.Range(0, constant.hatName.Length);
        GameObject hat = ObjectPoolPro.Instance.GetFromPool(constant.hatName[hatSelected]);
        hat.transform.position = headPoint.position;
        hat.SetActive(true);
        hat.transform.SetParent(headPoint);
    }
    public void RenderShieldToHold()
    {
        int shieldSelected = Random.Range(0, constant.shieldName.Length);
        GameObject shield = ObjectPoolPro.Instance.GetFromPool(constant.shieldName[shieldSelected]);
        shield.transform.SetParent(shieldPoint);
        shield.transform.localPosition = Vector3.zero;
        shield.transform.localRotation = Quaternion.identity;
        shield.SetActive(true);
    }
    public void AddHeadPoint()
    {
        levelHeadPoint = Instantiate(headpointPrefab); //ObjectPoolPro.Instance.GetFromPool(Constant.HEADPOINT);

        levelHeadPoint.SetOwner(this);
        levelHeadPoint.ChangePointText(enemyKilled);
        levelHeadPoint.gameObject.SetActive(true);


    }
    public void ChangeColorIndicator()
    {
        targetIndicator.TargetColor = skin.material.color;
    }
}
