using UnityEngine;

public class Slave : Creature
{
    [SerializeField] private float _damageUp;

    public override bool TryCastSpell()
    {
        _damage += _damageUp;
        return true;
    }
}
