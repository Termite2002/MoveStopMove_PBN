using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.agent.speed = 0;
        bot.agent.isStopped = true;
        bot.agent.destination = bot.TF.position;
    }

    public void OnExecute(Bot bot)
    {
        if (!bot.IsDead)
        {
            bot.ChangeState(new IdleState());
            if (bot.agent.speed > 0)
            {
                bot.ChangeState(new PatrolState());
            }
        }
        
    }

    public void OnExit(Bot bot)
    {
        
    }
}
