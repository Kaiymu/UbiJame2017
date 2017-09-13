using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusCol : MonoBehaviour {

    public GameManager.Bonus bonus = GameManager.Bonus.NONE;

	public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            OnPlayerCol(col.GetComponent<Player>());
        }
    }

    protected virtual void OnPlayerCol(Player player)
    {
        //player.PlayerInventoryGet
    }
}
