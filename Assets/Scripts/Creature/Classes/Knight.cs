using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Creature
{
    [SerializeField] private float _castRadius;
    [SerializeField] private float _armorUp;
    [SerializeField] private float _durationSpell;
    [SerializeField] private int _numberTargetSpell;

    private int _currentNumber;

    public override void Upgrade()
    {
        base.Upgrade();
        _maxHealth += 150;
        _armor += 2.5f;
    }

    public override bool TryCastSpell()
    {
        List<Creature> creatures = new List<Creature>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _castRadius);
        foreach (var collider in colliders)
        {
            if (_currentNumber < _numberTargetSpell)
            {
                if (collider.GetComponent<Creature>())
                {
                    if (collider.GetComponent<Creature>().IsArmorBuff == false)
                    {
                        creatures.Add(collider.GetComponent<Creature>());
                        _currentNumber++;
                    }

                }
            }
        }

        if (creatures.Count > 0)
        {
            foreach (var creature in creatures)
            {
                creature.ArmorUp(_armorUp, _durationSpell);
                StartCoroutine(TimerBuff(creature, _armorUp, _durationSpell));
            }
            _currentNumber = 0;
            return true;
        }
        else
        {
            _currentNumber = 0;
            return false;
        }
    }

    IEnumerator TimerBuff(Creature creature, float armor, float duration)
    {
        yield return new WaitForSeconds(duration);
        creature.ArmorDebuff(_armorUp);
    }
}
