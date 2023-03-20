using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private List<Creature> _creatures;
    [SerializeField] private CreatureViewSelect _creatureViewSelect;
    [SerializeField] private GameObject _creatureContainer;

    private void Start()
    {
        for (int i = 0; i < _creatures.Count; i++)
        {
            AddCreature(_creatures[i]);
        }
    }

    private void AddCreature(Creature creature)
    {
        var view = Instantiate(_creatureViewSelect, _creatureContainer.transform);
        view.Render(creature);
    }
}
