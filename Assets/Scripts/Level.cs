using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private Transform _spawnerPoint;

    private List<Enemy> _enemies = new List<Enemy>();
    private Levels _levels;
    private bool _isPassed = false;

    public bool IsPassed => _isPassed;

    public List<Enemy> Enemies => _enemies;

    public Transform CameraPoint => _cameraPoint;

    public Transform SpawnerPoint => _spawnerPoint;

    public Levels Levels => _levels;

    public event UnityAction LevelPassed;

    private void Start()
    {
        _levels = GetComponentInParent<Levels>();
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void CheckPassageLevel()
    {
        int numberDeadEnemies = 0;

        foreach (Enemy enemy in _enemies)
        {
            if (enemy.IsAlive == false)
                numberDeadEnemies++;
        }
        if (numberDeadEnemies == _enemies.Count)
        {
            _isPassed = true;
            LevelPassed?.Invoke();
        }
    }
}
