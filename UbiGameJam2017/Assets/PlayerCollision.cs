using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Bonus")
        {
            var currentBonus = col.GetComponent<RandomCollectible>().Bonus;
            GetComponent<Player>().PlayerInventoryGet.SetCurrentBonus(currentBonus);

            if(col != null) 
                Destroy(col.gameObject);
        }
    }
}
