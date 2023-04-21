using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType { Axe = 0, Boomerang = 1, Sword = 2, Z = 3, Candy = 4, Hammer = 5, Uzi = 6};
public class Weapon : MonoBehaviour
{
    [SerializeField] protected float rotareSpeed;
    public WeaponType type;
    public bool playerOwner;
    public Player player;
    public Bot owner;

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
            bot.PlayParticle(1);
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
            player.PlayParticle(1);
            player.OnDespawn();
            LevelManager.Instance.WhenPlayerLose();

            if (SaveLoadController.Instance.vibrate == 1)
            {
                PhoneVibrate.Instance.VibrateDevice();
            }

            DestroyWeapon();
        }
    }
    protected virtual void DestroyWeapon()
    {
        ObjectPoolPro.Instance.ReturnToPool(type.ToString(), gameObject);
        gameObject.SetActive(false);
    }
    public void SetOwner(Bot bot) 
    {
        owner = bot;
    }
    void WhenPlayerKill(Bot bot)
    {
        SaveLoadController.Instance.gold++;
        LevelManager.Instance.coinGainInLevel++;
        bot.aim.SetActive(false);
        player.targetListInRange.Remove(bot);
        player.RefreshEnemyInRange();
        player.enemyKilled++;
        if (player.levelHeadPoint != null)
        {
            player.levelHeadPoint.ChangePointText(player.enemyKilled);
        }
        if (player.enemyKilled % 3 == 0)
        {

            player.PlayParticleForTime(2, 2);
            player.bodyScale.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            player.atkRange.TF.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            CameraFollow.Instance.camDistance += new Vector3(0f, -0.5f, 0.5f);

            //SoundManager.Instance.PlaySFX(2);
            SoundManager.Instance.PlaySound(7);
        }
    }
    void WhenBotKill()
    {
        //TODO: owner dang la character roi k can getcomponent nua (DONE)
        owner.enemyKilled++;
        if (owner.levelHeadPoint != null)
        {
            owner.levelHeadPoint.ChangePointText(owner.enemyKilled);
        }
        owner.RefreshEnemyInRange();
        if (owner.enemyKilled % 3 == 0)
        {
            owner.bodyScale.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
