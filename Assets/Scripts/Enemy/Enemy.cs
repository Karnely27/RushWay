using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _delay;
    [SerializeField] private float _range;
    [SerializeField] private float _goldReward;
    [SerializeField] private float _energyReward;
    [SerializeField] private GamePlayer _gamePlayer;

    private Level _parent;
    private Creature _target;
    private float _currentHealth;
    private bool _isAlive = true;

    public float Damage => _damage;

    public float Delay => _delay;

    public float Range => _range;

    public Creature Target => _target;

    public bool IsAlive => _isAlive;

    private void Start()
    {
        _parent = GetComponentInParent<Level>();
        _parent.AddEnemy(this);
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _isAlive = false;
            _gamePlayer.SetEnergy(_energyReward);
            _gamePlayer.SetReward(_goldReward);
            _parent.CheckPassageLevel();
            gameObject.SetActive(false);
        }
    }

    public void GetNearTarget()
    {
        Creature nearCreature = null;

        float shorterDistance = Mathf.Infinity;
        List<Creature> targets = new List<Creature>();

        
        if (_parent.Levels.Container.Creatures.Count != 0)
        {
            foreach (Creature creature in _parent.Levels.Container.Creatures)
            {
                targets.Add(creature);
            }

            foreach (Creature creature in targets)
            {
                if (creature.IsAlive == true)
                {
                    if (shorterDistance > Vector3.Distance(transform.position, creature.transform.position))
                    {
                        shorterDistance = Vector3.Distance(transform.position, creature.transform.position);
                        nearCreature = creature;
                    }
                }
            }

            if(shorterDistance <= _range)
            {
                _target = nearCreature;
            }
            else
            {
                _target = null;
            }
        }
        else
        {
            _target = null;
        }
    }
}
