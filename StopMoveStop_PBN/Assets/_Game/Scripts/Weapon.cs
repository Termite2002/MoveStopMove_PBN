using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType { Axe, Boomerang, Sword};
public class Weapon : MonoBehaviour
{
    [SerializeField] protected float rotareSpeed;
    public WeaponType type;
    protected Rigidbody rb;
    
    protected virtual void OnEnable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    protected virtual void OnDisable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    protected virtual void DestroyWeapon()
    {
        ObjectPoolPro.Instance.ReturnToPool(type.ToString(), gameObject);
        gameObject.SetActive(false);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            Bot bot = other.GetComponent<Bot>();
            bot.isDead = true;
            bot.ChangeState(new DeadState());
            bot.ChangeAnim("Dead");
            bot.GetComponent<Bot>().DestroyBot();
            if (type.ToString() == "Axe")
            {
                DestroyWeapon();
            }
        }
    }
}
