using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType { Axe = 7, Boomerang = 6, Sword = 8};
public class Weapon : MonoBehaviour
{
    [SerializeField] protected float rotareSpeed;
    public WeaponType type;
    public bool playerOwner;
    public Player player;
    public Character owner;

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
    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        // Collide with bot
        if (other.CompareTag("Bot"))
        {
            Bot bot = other.GetComponent<Bot>();
            bot.isDead = true;
            bot.ChangeState(new DeadState());
            bot.ChangeAnim("Dead");
            bot.GetComponent<Bot>().DestroyBot();
            
            DestroyWeapon();
            
            // Check vu khi cua ai
            if (playerOwner)
            {
                WhenPlayerKill();
                SoundManager.Instance.PlaySFX(6);
            }
            else
            {
                WhenBotKill();
            }
        }

        // Collide with player
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.isDead = true;

            SoundManager.Instance.PlaySFX(3);
        }
    }
    protected virtual void DestroyWeapon()
    {
        ObjectPoolPro.Instance.ReturnToPool(type.ToString(), gameObject);
        gameObject.SetActive(false);
    }
    public void setOwner(Character bot) 
    {
        owner = bot;
    }
    void WhenPlayerKill()
    {
        SaveLoadController.Instance.gold++;
        player.enemyKilled++;
        if (player.enemyKilled % 3 == 0)
        {
            player.atkRange.transform.localScale += Vector3.one;
            CameraFollow.Instance.camDistance += new Vector3(0f, -2.5f, 2.5f);

            SoundManager.Instance.PlaySFX(2);
        }
    }
    void WhenBotKill()
    {
        owner.GetComponent<Character>().enemyKilled++;
        owner.GetComponent<Character>().atkRange.transform.localScale += Vector3.one;
    }
}
