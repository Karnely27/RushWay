using UnityEngine;

public class AttackState : State
{
    private float _currentDelay;

    private void Update()
    {
        if (_currentDelay >= Creature.Delay)
        {
            Attack(Creature.Target);
            _currentDelay = 0;
        }
        else
        {
            _currentDelay += Time.deltaTime;
        }
    }

    private void Attack(Enemy target)
    {
        if (target.IsAlive == true)
        {
            target.ApplyDamage(Creature.Damage);
        }
    }
}
