using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOtherPlayer : MonoBehaviour {

    public GameObject currentPlayer;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject != currentPlayer && other.GetComponent<Collider2D>().tag == "Player") {
            GameManager.Instance.KillPlayer(other.gameObject.GetComponent<Player>());
        }
    }
}
