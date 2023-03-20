using UnityEngine;

public class DistanceTransition : Transition
{
    private void Update()
    {
        if (Creature.Target != null)
        {
            if (Vector3.Distance(transform.position, Creature.Target.transform.position) < Creature.Range)
                NeedTransit = true;
        }
    }
}
