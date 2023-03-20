using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreatureViewShop : MonoBehaviour
{
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _selectButton;
    [SerializeField] private TMP_Text _levelUpgrade;
    [SerializeField] private GameObject _maxLevel;
    [SerializeField] private GameObject _buy;

    private Creature _creature;

    public event UnityAction<Creature, CreatureViewShop> SellButtonClick; 

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Creature creature)
    {
        _creature = creature;
        _lable.text = creature.Lable;
        _price.text = creature.PriceGold.ToString();
        _icon.sprite = creature.Icon;
        _levelUpgrade.text = creature.LevelUpgrade.ToString();
    }

    public void SetButtonMaxLevel()
    {
        _buy.SetActive(false);
        _maxLevel.SetActive(true);
    }

    public void Refresh()
    {
        Render(_creature);
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_creature, this);
    }
}
