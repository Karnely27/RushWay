using UnityEngine;

public class MoveState : State
{
    private void Update()
    {
        if (Creature.Target != null)
        {
            transform.LookAt(Creature.Target.transform);
            transform.position = Vector3.MoveTowards(transform.position, Creature.Target.transform.position, Time.deltaTime * Creature.Speed);
        }
    }
}
