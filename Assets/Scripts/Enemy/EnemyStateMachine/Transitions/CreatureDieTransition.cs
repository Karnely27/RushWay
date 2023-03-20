using UnityEngine;

public class CreatureDieTransition : EnemyTransition
{
    private void Update()
    {
        if (Enemy.Target == null || Enemy.Target.IsAlive == false)
        {
            NeedTransit = true;
        }
    }
}
