using UnityEngine;

public class FindCreatureState : EnemyState
{
    private void FixedUpdate()
    {
        if (Enemy.Target == null || Enemy.Target.IsAlive == false)
        {
            Enemy.GetNearTarget();
        }
    }
}
