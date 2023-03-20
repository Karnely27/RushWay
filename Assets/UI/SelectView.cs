using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectView : MonoBehaviour
{
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private TMP_Text _levelUpgrade;

    private Creature _creature;

    private void OnEnable()
    {
        _deleteButton.onClick.AddListener(OnButtonClick);
    }
    public void Render(Creature creature)
    {
        _creature = creature;
        _lable.text = creature.Lable;
        _icon.sprite = creature.Icon;
        _levelUpgrade.text = creature.LevelUpgrade.ToString();
    }

    private void OnButtonClick()
    {
        _creature.SetSelected();
        _deleteButton.onClick.RemoveListener(OnButtonClick);
        Destroy(gameObject);
    }
}
