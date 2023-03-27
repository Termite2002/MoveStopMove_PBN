using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<Character> targetListInRange = new List<Character>();
    public List<GameObject> weaponList = new List<GameObject>();
    public Dictionary<string, GameObject> weaponDict = new Dictionary<string, GameObject>();
    public bool isPlayer;
    public AttackRange atkRange;

    [SerializeField] private Animator anim;
    private string currentAnimName;

    //public List<GameObject> weaponPrefabs = new List<GameObject>();
    [SerializeField] protected float throwForce = 400f;

    //public Transform targetPosition;
    [SerializeField] protected Transform throwPoint;

    protected bool isDead;
    //public bool IsDead => isDead;

    public bool IsDead { get => isDead; set => isDead = value; }

    public WeaponType currentWeapon;

    public int enemyKilled;

    public Transform handPoint;
    public GameObject currentWeaponToHold;

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

    protected virtual void Start()
    {
        //TODO: khong duoc dung (DONE)
        //atkRange = GetComponentInChildren<AttackRange>();
        AddWeaponToWeaponDict();
    }
    public virtual void AddWeaponToWeaponDict()
    {
        weaponDict.Add(Constant.WEAPON_AXE, weaponList[0]);
        weaponDict.Add(Constant.WEAPON_BOOMERANG, weaponList[1]);
        weaponDict.Add(Constant.WEAPON_SWORD, weaponList[2]);
    }
    public void RefreshEnemyInRange()
    {
        for (int i = 0; i < targetListInRange.Count; i++)
        {
            if (Vector3.Distance(TF.position, targetListInRange[i].TF.position) - (atkRange.radius * atkRange.TF.localScale.x) > 1f)
            {
                targetListInRange.RemoveAt(i);
            }
        }

        targetListInRange.RemoveAll(Character => Character == null);
        targetListInRange.RemoveAll(Character => Character.IsDead);
    }
    public virtual void Attack(Vector3 targetPosition) 
    {
        if (!isDead && Vector3.Distance(TF.position, targetPosition) - (atkRange.radius * atkRange.TF.localScale.x) < 0.1f)
        {
            //Debug.Log(atkRange.radius * atkRange.TF.localScale.x);
            Vector3 throwDirection = (targetPosition - throwPoint.position).normalized;
            //transform.rotation = Quaternion.LookRotation(throwDirection);
            tf.LookAt(targetPosition);
            StartCoroutine(IEThrow(targetPosition));
        }
    }
    public IEnumerator IEThrow(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(0.5f);
        Weapon weapon = Cache.GetWeapon(ObjectPoolPro.Instance.GetFromPool(currentWeapon.ToString()));
        weapon.TF.position = throwPoint.position;
        weapon.gameObject.SetActive(true);


        // Kiem tra xem vu khi co phai cua player ? 
        if (isPlayer)
        {
            weapon.SetOwner(null);
            weapon.playerOwner = true;

            //SoundManager.Instance.PlaySFX(1);
            SoundManager.Instance.PlaySound(1);
        }
        else
        {
            weapon.SetOwner(this);
            weapon.playerOwner = false;
        }

        // Xac dinh huong nem
        Vector3 throwDirection = (targetPosition - throwPoint.position).normalized;
        //TODO: fix this (DONE)

        if (weapon.type == WeaponType.Sword)
        {
            //weapon.TF.LookAt(throwDirection);
            //weapon.GetComponent<Sword>().onAttack = true;
            //weapon.GetComponent<Sword>().throwDirection = throwDirection;
            weapon.TF.rotation = TF.rotation;
        }
        weapon.RB.AddForce(throwDirection * throwForce);



    }
    public virtual void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public virtual Vector3 FindNearestBotInRange()
    {
        Vector3 nearestBotPotition = Vector3.zero;
        float closestDistance = Mathf.Infinity;
        Character removeCharacter = new Character();

        foreach (Character character in targetListInRange)
        {
            if (character != null)
            {
                //TODO: khong can getcomponent character o day'  (DONE)
                //cache transform (DONE)
                float distance = Vector3.Distance(character.TF.position, TF.position);

                if (distance < closestDistance)
                {
                    removeCharacter = character;
                    closestDistance = distance;
                    nearestBotPotition = character.TF.position;
                }
            }
        }
        //targetListInRange.Remove(removeCharacter);
        return nearestBotPotition;
    }

    public void RenderWeaponToHold()
    {
        if (currentWeaponToHold == null)
        {
            currentWeaponToHold = Instantiate(weaponDict[currentWeapon.ToString()], handPoint);
        }
        else
        {
            GameObject weaponTemp = currentWeaponToHold;
            Destroy(weaponTemp);
            currentWeaponToHold = Instantiate(weaponDict[currentWeapon.ToString()], handPoint);
        }
    }
}
