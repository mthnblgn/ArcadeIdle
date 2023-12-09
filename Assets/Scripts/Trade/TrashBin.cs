using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TrashBin : MonoBehaviour
{
    float counter=1;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Actor actor))
        {        
            Player player = other.GetComponent<Player>();
            player.Countdown(counter);
            if (counter <= 0)
            {
                actor.SellIngredient(5);
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
}
