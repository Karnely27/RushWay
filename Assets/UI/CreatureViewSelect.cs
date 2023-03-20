using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreatureViewSelect : MonoBehaviour
{
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private GameObject _blockImage;
    [SerializeField] private GameObject _upgradeImage;
    [SerializeField] private TMP_Text _lvlUpgrade;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _select;
    [SerializeField] private Button _selectButton;

    private Creature _creature;

    public event UnityAction<Creature> OnSelected;
    public event UnityAction<Creature> OnDeleted;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnButtonClick);
        _creature.OnSelected += DeleteSelect;
        _lvlUpgrade.text = _creature.LevelUpgrade.ToString();

        if (_creature.IsBuyed && _creature.IsSelected == false)
        {
            _blockImage.SetActive(false);
            _select.SetActive(true);
        }
        else
        {
            _select.SetActive(false);
            _blockImage.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnButtonClick);
        _creature.OnSelected -= DeleteSelect;
    }

    public void Render(Creature creature)
    {
        _creature = creature;
        _lable.text = creature.Lable;
        _icon.sprite = creature.Icon;
    }

    private void OnButtonClick()
    {
        OnSelected?.Invoke(_creature);
        _select.SetActive(false);      
        _blockImage.SetActive(true);
        _creature.SetSelected();
    }

    private void DeleteSelect()
    {
        OnDeleted?.Invoke(_creature);
        _blockImage.SetActive(false);
        _select.SetActive(true);      
    }
}
