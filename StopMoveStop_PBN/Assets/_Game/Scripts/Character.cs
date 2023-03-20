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

    public bool isDead;
    public WeaponType currentWeapon;

    public int enemyKilled;

    public Transform handPoint;
    public GameObject currentWeaponToHold;

    protected virtual void Start()
    {
        atkRange = GetComponentInChildren<AttackRange>();
        AddWeaponToWeaponDict();
    }
    public virtual void AddWeaponToWeaponDict()
    {
        weaponDict.Add("Axe", weaponList[0]);
        weaponDict.Add("Boomerang", weaponList[1]);
        weaponDict.Add("Sword", weaponList[2]);
    } 
    public virtual void Attack(Vector3 targetPosition) 
    {
        if (!isDead)
        {
            Vector3 throwDirection = (targetPosition - throwPoint.position).normalized;
            transform.rotation = Quaternion.LookRotation(throwDirection);
            StartCoroutine(Throw(targetPosition));
        }
    }
    public IEnumerator Throw(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject weapon = ObjectPoolPro.Instance.GetFromPool(currentWeapon.ToString());
        weapon.transform.position = throwPoint.position;
        weapon.SetActive(true);


        // Kiem tra xem vu khi co phai cua player ? 
        if (isPlayer)
        {
            weapon.GetComponent<Weapon>().setOwner(null);
            weapon.GetComponent<Weapon>().playerOwner = true;

            SoundManager.Instance.PlaySFX(1);
        }
        else
        {
            weapon.GetComponent<Weapon>().setOwner(this);
            weapon.GetComponent<Weapon>().playerOwner = false;
        }

        // Xac dinh huong nem
        Vector3 throwDirection = (targetPosition - throwPoint.position).normalized;
        weapon.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce);

        if (weapon.GetComponent<Weapon>().type == WeaponType.Sword)
        {
            weapon.transform.rotation = Quaternion.LookRotation(throwDirection);
        }

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
                float distance = Vector3.Distance(character.GetComponent<Character>().transform.position, transform.position);

                if (distance < closestDistance)
                {
                    removeCharacter = character;
                    closestDistance = distance;
                    nearestBotPotition = character.GetComponent<Character>().transform.position;
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
