using UnityEngine;

public class Sniper : Creature
{
    [SerializeField] private float _criticalPercent;

    public override bool TryCastSpell()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) <= Range)
        {
            float critDamage = (Damage * _criticalPercent) / 100;
            Target.ApplyDamage(critDamage);
            return true;
        }
        else
        {
            return false;
        }
    }
}
