using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    public void OnEnter(Bot bot)
    {
        if (bot.IsDead)
        {
            bot.ChangeState(new DeadState());
        }
        bot.RefreshEnemyInRange();
        if (bot.targetListInRange.Count > 0)
        {
            bot.StopMoving();
            bot.Attack(bot.FindNearestBotInRange());
        }
        timer = 0;
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer >= 2f && !bot.IsDead)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
