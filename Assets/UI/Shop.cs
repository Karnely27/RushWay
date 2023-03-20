using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Creature> _creatures;
    [SerializeField] private Player _player;
    [SerializeField] private CreatureViewShop _creatureView;
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
        var view = Instantiate(_creatureView, _creatureContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(creature);
    }

    private void OnSellButtonClick(Creature creature, CreatureViewShop view)
    {
        TrySellCreature(creature, view);
    }

    private void TrySellCreature(Creature creature, CreatureViewShop view)
    {
        if (creature.IsBuyed == false)
        {
            if (creature.PriceGold <= _player.Money)
            {
                _player.BuyCreature(creature);
                creature.Buy();
                creature.SetNewPrice();
                view.Refresh();
            }
        }
        else
        {
            if (creature.PriceGold <= _player.Money)
            {
                _player.BuyCreature(creature);
                creature.Upgrade();
                view.Refresh();
            }
        }
        if (creature.LevelUpgrade >= 3)
        {
            view.SetButtonMaxLevel();
            view.Refresh();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}
