using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private float timer;
    private float randomTime;
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
        timer = 0;
        randomTime = Random.Range(0.5f, 1f);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (bot.targetListInRange.Count > 0 && !bot.IsDead)
        {
            bot.ChangeState(new AttackState());
        }
        if (timer > randomTime)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
