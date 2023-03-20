using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    private List<Creature> _creatures = new List<Creature>();

    public List<Creature> Creatures => _creatures;

    public void AddCreature(Creature creature)
    {
        _creatures.Add(creature);
    } 
}
