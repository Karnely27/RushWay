using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Levels : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Container _container;

    private Level _currentLevel;

    public Spawner Spawner => _spawner;

    public Container Container => _container;

    public Level CurrentLevel => _currentLevel;

    public event UnityAction LevelWasSetted;

    private void OnEnable()
    {
        GetNextLevel();
    }

    private void Update()
    {
        if (_currentLevel.IsPassed == true)
            GetNextLevel();
    }

    private void GetNextLevel()
    {
        foreach (var level in _levels)
        {
            if (level.IsPassed == false)
            {
                _currentLevel = level;
                LevelWasSetted?.Invoke();
                return;
            }
        }
    }
}
