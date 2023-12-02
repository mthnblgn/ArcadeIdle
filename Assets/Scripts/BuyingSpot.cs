using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyingSpot : MonoBehaviour
{
    [SerializeField] SourcesScriptable _whatToBuy;
    [SerializeField] Source _sourcePrefab;
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _priceText;
    int _currentPrice = 0;
    float counter = 2;
    void Start()
    {
        _image.color = _whatToBuy._ingredientMat.color;
        _currentPrice = _whatToBuy._sourcePrice;
        _priceText.text = _currentPrice.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (counter <= 0)
            {
                StartCoroutine(BuyingCoroutine(player));
            }
            else counter -= Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        counter = 2;
    }

    IEnumerator BuyingCoroutine(Player p)
    {
        if (p.money > 0)
        {
            p.money--;
            _currentPrice--;
            _priceText.text = _currentPrice.ToString();
            if (_currentPrice == 0)
            {
                Source s = Instantiate(_sourcePrefab, transform.position, Quaternion.identity);
                s.SetSource(_whatToBuy);
                ResetAndRelocate();
            }
            yield return new WaitForSeconds(.05f);
        }
        yield return null;
    }
    void ResetAndRelocate()
    {
        _currentPrice = _whatToBuy._sourcePrice;
        _priceText.text = _currentPrice.ToString();
        transform.position += Vector3.forward * 6;
    }
}
