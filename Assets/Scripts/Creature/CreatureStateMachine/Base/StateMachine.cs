using UnityEngine;

[RequireComponent(typeof(Creature))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;
    private Creature _creature;

    private void Start()
    {
        _creature = GetComponent<Creature>();
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    public void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_creature);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = nextState;


        if (_currentState != null)
        {
            _currentState.Enter(_creature);
        }
    }
}