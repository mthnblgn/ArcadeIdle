using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SellingSpot : MonoBehaviour
{
    float counter = 1;
    public Customer[] _customers;

    private void Awake()
    {
        _customers = new Customer[3];
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Actor actor) && actor.IsCarry() && CheckOrder(actor))
        {
            Player player = other.GetComponent<Player>();
            player.Countdown(counter);
            if (counter <= 0)
            {
                if (_customers[0]!=null)StartCoroutine(SellingCoroutine(player, actor));
                counter = 1;
            }
            else counter -= Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.TryGetComponent(out Player player);
        counter = 1;
        player.Countdown(0);
    }
    IEnumerator SellingCoroutine(Player p, Actor a)
    {
        int orderCount = _customers[0]._order.Length;
        Carrier[] carriers = a.ReturnLastCarryPoints(orderCount);
        CustomersReset();
        foreach (Carrier c in carriers)
        {
            p.Sell(c._ingredient.sellValue);
        }
        a.SellIngredient(orderCount);
        yield return new WaitForSeconds(.5f);
    }

    private void CustomersReset()
    {
        Customer[] _newLine = new Customer[2];
        _newLine[0] = _customers[1];
        _newLine[1] = _customers[2];
        _customers[0].GetOutLine();
        _customers[0] = null;
        _customers[1] = null;
        _customers[2] = null;
        _newLine[0]?.GetInLine(this);
        _newLine[1]?.GetInLine(this);
        GameController._customerCount++;
    }

    public void AddCustomer(Customer c)
    {
        for (int i = 0; i < _customers.Length; i++)
        {
            if (_customers[i] == null)
            {
                _customers[i] = c;
                break;
            }
        }
    }
    public int CustomerCount()
    {
        int l = 0;
        for (int i = 0; i < _customers.Length; i++)
        {
            if (_customers[i] != null) l++;
        }
        return l;
    }
    public bool CheckOrder(Actor actor)
    {
        if (actor == null || _customers[0] == null)
        {
            return false;
        }

        int[] carry = actor.CarryIDs();
        int[] order = _customers[0]._order;


        if (carry.Reverse().Skip(actor.EmptyCarryCount()).Take(order.Length).SequenceEqual(order))
        {
            return true;
        }


        return false;
    }
}
