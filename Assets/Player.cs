using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    private float _money;

    public float Money => _money;

    private void OnEnable()
    {
        //для рестарта денег, для начала игры
        //PlayerPrefs.SetFloat("Money", 0); 
        //PlayerPrefs.SetFloat("MoneyPlayer", 0);
        float moneyAfterGeme = PlayerPrefs.GetFloat("Money");
        float moneyPlayer = PlayerPrefs.GetFloat("MoneyPlayer");
        _money += moneyPlayer + moneyAfterGeme;
        ShowMoney(_money);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MoneyPlayer", _money);
        PlayerPrefs.SetFloat("Money", 0);
    }

    public void BuyCreature(Creature creature)
    {
        _money -= creature.PriceGold;
        ShowMoney(_money);
    }

    private void ShowMoney(float money)
    {
        _moneyText.text = money.ToString();
    }
}
