using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] private string _lable;
    [SerializeField] private float _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private float _delay;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _armor;
    [SerializeField] protected float _maxHealth;
    [SerializeField] private LayerMask _enemiesLayer;
    [SerializeField] private float _maxMana;
    [SerializeField] private float _recoveryManaPerSecond;
    [SerializeField] private float _energyCost;
    [SerializeField] private float _energyReturn;

    private GamePlayer _gamePlayer;
    private Levels _levels;
    private Level _level;
    private Enemy _target;
    private int _levelUpgrade;
    private float _currentMana;
    private float _currentTime;
    private float _currentHealth;
    private bool _isAlive = true;
    private bool _isArmorBuff = false;
    private bool _isBuyed = false;
    private bool _isSelected = false;

    public string Lable => _lable;

    public float PriceGold => _price;

    public Sprite Icon => _icon;

    public bool IsSelected => _isSelected;

    public bool IsArmorBuff => _isArmorBuff;

    public int LevelUpgrade => _levelUpgrade;

    public bool IsBuyed => _isBuyed;

    public float Speed => _speed;

    public float Energy => _energyCost;

    public float Range => _range;

    public float Delay => _delay;

    public float Damage => _damage;

    public Enemy Target => _target;

    public bool IsAlive => _isAlive;

    public event UnityAction OnSelected;

    private void OnDisable()
    {
        _levels.LevelWasSetted -= SetNextLevel;
    }

    private void Start()
    {
        _currentMana = _maxMana / 2;
    }

    private void Update()
    {
        if (_currentMana < _maxMana)
        {
            if (_currentTime < 1)
                _currentTime += Time.deltaTime;
            else
            {
                _currentTime = 0;
                _currentMana += _recoveryManaPerSecond;
                if (_currentMana > _maxMana)
                    _currentMana = _maxMana;
            }
        }
        if (_currentMana >= _maxMana)
        {
            if (TryCastSpell())
                _currentMana = 0;
        }
    }

    public virtual void Upgrade()
    {
        _levelUpgrade++;
        SetNewPrice();
    }

    public virtual bool TryCastSpell() { return false; }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage - ((damage * _armor) / 100);

        if (_currentHealth <= 0)
        {
            _isAlive = false;
            _gamePlayer.SetEnergy(_energyReturn);
            gameObject.SetActive(false);
        }
    }

    public void Init(Levels levels, GamePlayer gamePlayer)
    {
        _currentHealth = _maxHealth;
        _gamePlayer = gamePlayer;
        _levels = levels;
        SetNextLevel();
        _levels.LevelWasSetted += SetNextLevel;
        _levels.Container.AddCreature(this);
    }

    public void SetNextLevel()
    {
        _level = _levels.CurrentLevel;
    }

    public void GetNearTarget()
    {
        Enemy nearEnemy = null;
        Enemy nearBlock = null;

        float shorterDistanceEnemy = Mathf.Infinity;
        float shorterDistanceBlock = Mathf.Infinity;
        List<Enemy> targets = new List<Enemy>();

        if (_level.Enemies.Count != 0)
        {
            for (int i = 0; i < _level.Enemies.Count; i++)
            {
                targets.Add(_level.Enemies[i]);
            }

            foreach (Enemy enemy in targets)
            {
                if (enemy.IsAlive == true)
                {
                    if (enemy.GetComponent<PistolMan>())
                    {
                        if (shorterDistanceEnemy > Vector3.Distance(transform.position, enemy.transform.position))
                        {
                            shorterDistanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                            nearEnemy = enemy;
                        }
                    }
                    if (enemy.GetComponent<Barricades>())
                    {
                        if (shorterDistanceBlock > Vector3.Distance(transform.position, enemy.transform.position))
                        {
                            shorterDistanceBlock = Vector3.Distance(transform.position, enemy.transform.position);
                            nearBlock = enemy;
                        }
                    }
                }
            }

            if (nearEnemy != null)
            {
                Ray ray = new Ray(transform.position, nearEnemy.transform.position - transform.position);
                RaycastHit hit;

                if (_range <= 1 && Physics.Raycast(ray, out hit, 1000f, _enemiesLayer))
                {
                    if (hit.collider.gameObject.GetComponent<PistolMan>())
                    {
                        if (shorterDistanceEnemy < shorterDistanceBlock || nearBlock == null)
                        {
                            _target = nearEnemy;
                            return;
                        }
                    }
                    _target = nearBlock;
                    return;
                }
                if (_range > 1 && Physics.Raycast(ray, out hit, 1000f, _enemiesLayer))
                {
                    if (hit.collider.gameObject.GetComponent<PistolMan>())
                    {
                        if (shorterDistanceEnemy < shorterDistanceBlock || nearBlock == null)
                        {
                            _target = nearEnemy;
                            return;
                        }
                        if (Vector3.Distance(nearBlock.transform.position, nearEnemy.transform.position) < _range - 0.5f)
                        {
                            _target = nearEnemy;
                            return;
                        }
                    }
                    _target = nearBlock;
                    return;
                }
                _target = nearBlock;
                return;
            }
            else
            {
                _target = nearBlock;
                return;
            }
        }
        else
        {
            _target = null;
            return;
        }
    }

    public void ArmorUp(float armor, float duration)
    {
        _isArmorBuff = true;
        _armor += armor;
    }

    public void ArmorDebuff(float armor)
    {
        _isArmorBuff = false;
        _armor -= armor;
    }

    public void Buy()
    {
        _isBuyed = true;
        _levelUpgrade++;
    }

    public void SetNewPrice()
    {
        _price += (_price + 10) * 2;
    }

    public void SetSelected()
    {
        if (_isSelected)
        {
            _isSelected = false;
            OnSelected?.Invoke();
        }
        else
        {
            _isSelected = true;
        }
    }
}
