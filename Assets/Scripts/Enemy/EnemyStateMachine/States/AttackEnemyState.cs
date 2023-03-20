using UnityEngine;

public class AttackEnemyState : EnemyState
{
    private float _currentDelay;

    private void Update()
    {
        if (_currentDelay >= Enemy.Delay)
        {
            Attack(Enemy.Target);
            _currentDelay = 0;
        }
        else
        {
            _currentDelay += Time.deltaTime;
        }
    }

    private void Attack(Creature target)
    {
        if (target.IsAlive == true)
        {
            target.ApplyDamage(Enemy.Damage);
        }
    }
}
