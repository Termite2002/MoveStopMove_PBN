using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveVector;
    protected Joystick joystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Player player;
    private Rigidbody rb;
    [SerializeField] private bool oneHit;

    [SerializeField] private Animator anim;
    private string currentAnimName = Constant.ANIM_IDLE;

    [SerializeField] private float attackTime;
    private float timer;

    private Transform tf;
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

    void Start()
    {
        player = GetComponent<Player>();
        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody>();
        oneHit = false;
        timer = 0;
    }

    void FixedUpdate()
    {

        if (!player.IsDead)
        {
            timer -= Time.deltaTime;
            // When stop moving
            if (joystick.Horizontal == 0 && joystick.Vertical == 0)
            {
                ChangeAnim(Constant.ANIM_IDLE);
                if (oneHit == true && player.targetListInRange.Count > 0)
                {
                    player.RefreshEnemyInRange();
                    if (player.targetListInRange.Count > 0)
                    {
                        player.Attack(player.FindNearestBotInRange());
                        //Debug.Log("Tan cong");
                        ChangeAnim(Constant.ANIM_ATTACK);
                        oneHit = false;
                        timer = attackTime;
                    }
                }
            }
            // When moving
            else
            {
                if (timer <= 0)
                {
                    float angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
                    TF.rotation = Quaternion.Euler(0f, angle, 0f);

                    ChangeAnim(Constant.ANIM_RUN);
                    rb.velocity = new Vector3(joystick.Horizontal * moveSpeed,
                                    rb.velocity.y,
                                    joystick.Vertical * moveSpeed);
                    oneHit = true;
                }
            }
        }
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
