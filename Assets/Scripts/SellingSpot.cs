using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingSpot : MonoBehaviour
{
    float counter = 1;
    Order _order;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Actor actor) && actor.IsCarry())
        {
            Player player = other.GetComponent<Player>();
            player.Countdown(counter);
            if (counter <= 0)
            {
                StartCoroutine(SellingCoroutine(player, actor));
                counter = 1;
            }
            else { counter -= Time.deltaTime; }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.TryGetComponent(out Player player);
        counter = 1;
        player.Countdown(0);
    }
    private void OnTriggerEnter(Collider other)
    {
    }
    IEnumerator SellingCoroutine(Player p, Actor a)
    {

        p.Sell(a.FirstIngredientOnCarry().sellValue);
        a.SellIngredient();
        yield return new WaitForSeconds(.5f);
    }

    public void AssignOrder(Order order)
    {
        _order = order;
    }
}
