using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _levelText;


    private void Start()
    {
        LevelText= GameController._currentLevel.Level.ToString();
    }
    public string MoneyText { set { _moneyText.text ="Money: "+ value; } }
    public string LevelText { set { _levelText.text = "Level: " + value; } }

    public void LevelUp()
    {
        LevelText = GameController._currentLevel.Level.ToString();
        _levelText.transform.DOShakeRotation(1f, 90, 10, 90, true, ShakeRandomnessMode.Full);
    }
}
