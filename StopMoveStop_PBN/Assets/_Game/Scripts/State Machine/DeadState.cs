using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.agent.isStopped = true;
    }

    public void OnExecute(Bot bot)
    {
        if (!bot.IsDead)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
