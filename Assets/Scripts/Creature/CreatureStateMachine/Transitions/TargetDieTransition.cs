using UnityEngine;

public class TargetDieTransition : Transition
{
    private void Update()
    {
        if (Creature.Target.IsAlive == false || Creature.Target == null)
            NeedTransit = true;
    }
}
