using UnityEngine;

public class FindTargetState : State
{
    private void Update()
    {
        if(Creature.Target == null || Creature.Target.IsAlive == false)
        {
            Creature.GetNearTarget();
        }
    }
}
