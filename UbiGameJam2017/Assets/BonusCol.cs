using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusCol : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {

        }
    }

    protected virtual void OnPlayerCol(Player player)
    {

    }
}
