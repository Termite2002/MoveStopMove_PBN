using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private float timer;
    private float randomTime;
    public void OnEnter(Bot bot)
    {
        timer = 0;
        randomTime = Random.Range(4f, 5f);
        bot.ChangeTarget();
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if(timer < randomTime)
        {
            bot.Moving();
            if (timer > 0.5f && bot.targetListInRange.Count > 0 && !bot.isDead)
            {
                bot.ChangeState(new AttackState());
            }
        }
        else
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }

}
