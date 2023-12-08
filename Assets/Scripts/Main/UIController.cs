using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;

    public string MoneyText { set { _moneyText.text ="Money: "+ value; } }
}
