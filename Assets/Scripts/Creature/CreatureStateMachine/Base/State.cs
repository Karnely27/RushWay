using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Creature Creature { get; set; }

    public void Enter(Creature creature)
    {
        if (enabled == false)
        {
            Creature = creature;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(creature);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }
        return null;
    }
}