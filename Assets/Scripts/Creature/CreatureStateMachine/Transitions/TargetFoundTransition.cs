using UnityEngine;

public class TargetFoundTransition : Transition
{
    private void Update()
    {
        if (Creature.Target != null)
        {
            NeedTransit = true;
        }
    }
}
