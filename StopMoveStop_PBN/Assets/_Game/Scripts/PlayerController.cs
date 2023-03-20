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
    private bool oneHit;

    [SerializeField] private Animator anim;
    private string currentAnimName = "Idle";

    [SerializeField] private float attackTime;
    private float timer;
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

        if (!player.isDead)
        {
            timer -= Time.deltaTime;
            // When stop moving
            if (joystick.Horizontal == 0 && joystick.Vertical == 0)
            {
                ChangeAnim("Idle");
                if (oneHit == true && player.targetListInRange.Count > 0)
                {
                    player.Attack(player.FindNearestBotInRange());
                    //Debug.Log("Tan cong");
                    ChangeAnim("Attack");
                    oneHit = false;
                    timer = attackTime;
                }
            }
            // When moving
            else
            {
                if (timer <= 0)
                {
                    float angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    ChangeAnim("Run");
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
