using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyingSpot : MonoBehaviour
{
    [SerializeField] SourcesScriptable _sourceData;
    [SerializeField] Source _sourcePrefab;
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _priceText;
    int _currentPrice = 0;
    float counter = 1;
    void Start()
    {
        _image.color = _sourceData.ingredientData._ingredientMat.color;
        _currentPrice = _sourceData._sourcePrice;
        _priceText.text = _currentPrice.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player) && player.money > 0)
        {
            player.Countdown(counter);
            if (counter <= 0)
            {
                StartCoroutine(BuyingCoroutine(player));
            }
            else counter -= Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        counter = 1;
        other.TryGetComponent(out Player player);
        player.Countdown(0);
    }


    IEnumerator BuyingCoroutine(Player p)
    {

        p.money--;
        _currentPrice--;
        _priceText.text = _currentPrice.ToString();
        if (_currentPrice == 0)
        {
            Source s = Instantiate(_sourcePrefab, transform.position, Quaternion.identity);
            s.SetSource(_sourceData);
            ResetAndRelocate();
        }
        yield return new WaitForSeconds(.05f);
    }
    void ResetAndRelocate()
    {
        _currentPrice = _sourceData._sourcePrice;
        _priceText.text = _currentPrice.ToString();
        transform.position += Vector3.forward * 6;
    }
}
