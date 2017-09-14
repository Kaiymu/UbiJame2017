using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Bonus")
        {
            var playerBonus = GetComponent<Player>().PlayerInventoryGet;

            if (playerBonus.bonus != GameManager.Bonus.NONE)
                return;

            var currentBonus = col.GetComponent<RandomCollectible>().Bonus;

            playerBonus.SetCurrentBonus(currentBonus);

            if(col != null) 
                Destroy(col.gameObject);
        }
    }
}
