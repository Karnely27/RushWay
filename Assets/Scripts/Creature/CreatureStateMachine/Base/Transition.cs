using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Creature Creature { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Init(Creature creature)
    {
        Creature = creature;
    }
}