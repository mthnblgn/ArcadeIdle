using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int _money { get; set; }
    public int money
    {
        get { return _money; }
        set
        {
            _money = value;
            _uiController.MoneyText=_money.ToString();
            PlayerPrefs.SetInt(_moneyKey, _money);
        }
    }
    [SerializeField] Image _countdownImage;


    string _moneyKey = "Money";


    [SerializeField] UIController _uiController;
    void Start()
    {
        money = 400;//PlayerPrefs.GetInt(_moneyKey, 500);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Countdown(float value)
    {
        _countdownImage.fillAmount = value;
    }
    public void Sell(int value)
    {
        money += value;
    }
}
