using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _creaturesPrefabs;
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    [SerializeField] private Button _button3;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private TMP_Text _textEnergy;
    [SerializeField] private Levels _levels;
    [SerializeField] private Transform _container;

    private float _currentEnergy;
    private float _money;

    private Creature _creatureSelect1;
    private Creature _creatureSelect2;
    private Creature _creatureSelect3;

    private void OnEnable()
    {
        _creatureSelect1 = null;
        _creatureSelect2 = null;
        _creatureSelect3 = null;
        _button1.onClick.AddListener(OnButtonClick1);
        _button2.onClick.AddListener(OnButtonClick2);
        _button3.onClick.AddListener(OnButtonClick3);
    }

    private void OnDisable()
    {
        _button1.onClick.RemoveListener(OnButtonClick1);
        _button2.onClick.RemoveListener(OnButtonClick2);
        _button3.onClick.RemoveListener(OnButtonClick3);
        _creatureSelect1.SetSelected();
        _creatureSelect2.SetSelected();
        _creatureSelect3.SetSelected();
        PlayerPrefs.SetFloat("Money", _money);
    }

    private void Start()
    {
        SetCreatureInButton();
        _currentEnergy = _maxEnergy;
        ShowMoney(_money);
        ShowEnergy(_currentEnergy);
    }

    public void SetEnergy(float energy)
    {
        _currentEnergy += energy;
        ShowEnergy(_currentEnergy);
    }
   
    public void SetReward(float reward)
    {
        _money += reward;
        ShowMoney(_money);
    }

    private void OnButtonClick1()
    {
        TryInstantiateCreature(_creatureSelect1);
    }

    private void OnButtonClick2()
    {
        TryInstantiateCreature(_creatureSelect2);
    }

    private void OnButtonClick3()
    {
        TryInstantiateCreature(_creatureSelect3);
    }

    private void TryInstantiateCreature(Creature creature)
    {
        if (_currentEnergy >= creature.Energy)
        {
            _currentEnergy -= creature.Energy;
            ShowEnergy(_currentEnergy);
            Instant(creature);
        }
    }

    private void SetCreatureInButton()
    {
        foreach (var prefab in _creaturesPrefabs)
        {
            if (_creatureSelect1 == null)
            {
                if (prefab.GetComponent<Creature>().IsSelected == true)
                    _creatureSelect1 = prefab.GetComponent<Creature>();
            }
            else
            {
                if (_creatureSelect2 == null)
                {
                    if (prefab.GetComponent<Creature>().IsSelected == true)                   
                        _creatureSelect2 = prefab.GetComponent<Creature>();                   
                }
                else
                {
                    if (_creatureSelect3 == null)
                    {
                        if (prefab.GetComponent<Creature>().IsSelected == true)                       
                            _creatureSelect3 = prefab.GetComponent<Creature>();                       
                    }
                }
            }
        }
    }

    private void Instant(Creature creature)
    {
        Creature unit = creature;
        unit = Instantiate(creature, GetSpawnerPoint().position, Quaternion.identity, _container);
        unit.Init(_levels, this);
    }

    private Transform GetSpawnerPoint()
    {
        int index = Random.Range(0, _levels.Spawner.SpawnerPoints.Count - 1);
        Transform point = _levels.Spawner.SpawnerPoints[index];
        return point;
    }

    private void ShowEnergy(float currentEnergy)
    {
        _textEnergy.text = currentEnergy.ToString();
    }

    private void ShowMoney(float money)
    {
        _textMoney.text = money.ToString();
    }
}
