using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;

    private EnemyState _currentState;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    public void Reset(EnemyState startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_enemy);
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_enemy);
    }
}
