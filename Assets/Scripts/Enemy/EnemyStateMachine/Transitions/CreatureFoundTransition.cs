using UnityEngine;

public class CreatureFoundTransition : EnemyTransition
{
    private void Update()
    {
        if (Enemy.Target != null)
        {
            NeedTransit = true;
        }
    }
}
