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

    public Rigidbody RB
    {
        get
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            return rb;
        }
    }

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

    protected virtual void OnEnable()
    {
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
    }
    protected virtual void OnDisable()
    {
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
    }
    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        // Collide with bot
        //TODO: cache string (DONE)
        if (other.CompareTag(Constant.GAME_BOT))
        {
            Bot bot = Cache.GetCharacter(other) as Bot;
            bot.BloodHitEffect();
            bot.OnDespawn();
            
            DestroyWeapon();
            
            // Check vu khi cua ai
            if (playerOwner)
            {
                WhenPlayerKill(bot);
                SoundManager.Instance.PlaySound(6);
            }
            else
            {
                if (Vector3.Distance(owner.TF.position, bot.TF.position) > 0.1f)
                {
                    WhenBotKill();
                }
            }
        }

        // Collide with player
        if (other.CompareTag(Constant.GAME_PLAYER))
        {
            Player player = Cache.GetCharacter(other) as Player;
            player.BloodHitEffect();
            player.OnDespawn();
            LevelManager.Instance.WhenPlayerLose();

            DestroyWeapon();
        }
    }
    protected virtual void DestroyWeapon()
    {
        ObjectPoolPro.Instance.ReturnToPool(type.ToString(), gameObject);
        gameObject.SetActive(false);
    }
    public void SetOwner(Character bot) 
    {
        owner = bot;
    }
    void WhenPlayerKill(Character bot)
    {
        SaveLoadController.Instance.gold++;
        LevelManager.Instance.coinGainInLevel++;
        player.targetListInRange.Remove(bot);
        player.RefreshEnemyInRange();
        player.enemyKilled++;
        if (player.enemyKilled % 3 == 0)
        {
            player.atkRange.TF.localScale += Vector3.one;
            CameraFollow.Instance.camDistance += new Vector3(0f, -2.5f, 2.5f);

            //SoundManager.Instance.PlaySFX(2);
            SoundManager.Instance.PlaySound(2);
        }
    }
    void WhenBotKill()
    {
        //TODO: owner dang la character roi k can getcomponent nua (DONE)
        owner.enemyKilled++;
        owner.RefreshEnemyInRange();
        if (owner.enemyKilled % 3 == 0)
        {
            owner.atkRange.TF.localScale += Vector3.one;
        }
    }
}
