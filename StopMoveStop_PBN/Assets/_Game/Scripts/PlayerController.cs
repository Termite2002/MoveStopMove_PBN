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
    void Start()
    {
        player = GetComponent<Player>();
        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody>();
        oneHit = false;
    }
    void FixedUpdate()
    {

        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            if (oneHit == true && player.targetListInRange.Count > 0)
            {
                player.Attack(player.FindNearestBotInRange());
                oneHit = false;
            }
        }
        else
        {
            rb.velocity = new Vector3(joystick.Horizontal * moveSpeed,
                            rb.velocity.y,
                            joystick.Vertical * moveSpeed);
            oneHit = true;
        }
        //if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        //{
        //    PlayerController.instance.ChangeAnim("Idle");
        //}
        //else
        //{
        //    float angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
        //    PlayerController.instance.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //    PlayerController.instance.ChangeAnim("Run");
            //    transform.position += new Vector3(joystick.Horizontal * moveSpeed * Time.deltaTime, 0, joystick.Vertical * moveSpeed * Time.deltaTime);
            //}
    }
}
