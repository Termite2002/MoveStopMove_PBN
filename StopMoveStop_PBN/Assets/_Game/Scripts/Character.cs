using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<Character> targetListInRange = new List<Character>();
    public bool isPlayer;
    [SerializeField] private Animator anim;
    private string currentAnimName;

    //public List<GameObject> weaponPrefabs = new List<GameObject>();
    [SerializeField] protected float throwForce = 400f;

    //public Transform targetPosition;
    [SerializeField] protected Transform throwPoint;

    [SerializeField] private LevelController levelController;

    public bool isDead;
    public WeaponType currentWeapon;
    public virtual void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        levelController.allAlivePosition.Add(this);
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

        Vector3 throwDirection = (targetPosition - throwPoint.position).normalized;
        weapon.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce);

        //weapon.transform.rotation = Quaternion.LookRotation(throwDirection);
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
}
