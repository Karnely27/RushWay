using UnityEngine;

[RequireComponent(typeof(Levels))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Levels _levels;
    private Camera _camera;
    private Spawner _spawner;
    private Vector3 _targetCamera;
    private Vector3 _targetSpawner;

    private void OnEnable()
    {
        _camera = Camera.main;
        _levels = GetComponent<Levels>();
        _spawner = _levels.Spawner;
        Move();
        _levels.LevelWasSetted += Move;
    }

    private void OnDisable()
    {
        _levels.LevelWasSetted -= Move;
    }

    private void Update()
    {
        if(_camera.transform.position != _targetCamera)
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _targetCamera, Time.deltaTime * _speed);
        }
        if(_spawner.transform.position != _targetSpawner)
        {
            _spawner.transform.position = Vector3.MoveTowards(_spawner.transform.position, _targetSpawner, Time.deltaTime * _speed);
        }
    }

    private void Move()
    {
        _targetCamera = _levels.CurrentLevel.CameraPoint.position;
        _targetSpawner = _levels.CurrentLevel.SpawnerPoint.position;
    }
}
