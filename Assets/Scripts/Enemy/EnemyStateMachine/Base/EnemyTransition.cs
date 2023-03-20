using UnityEngine;

public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    protected Enemy Enemy { get; private set; }

    public EnemyState TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Init(Enemy enemy)
    {
        Enemy = enemy;
    }
}
