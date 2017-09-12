using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private List<Player> _playerList = new List<Player>();

    public void AddPlayer(Player player) {
        _playerList.Add(player);
    }

    public void KillPlayer(Player player) {
        Debug.LogError(player);
        if (_playerList.Contains(player)) {
            Debug.LogError(player);
            player.GetComponent<CarMovement>().stopMoving = true;

            StartCoroutine(StopPlayerMov(3, player));
        }
    }

    private IEnumerator StopPlayerMov(float timeToWait, Player player) {

        yield return new WaitForSeconds(timeToWait);
        player.GetComponent<CarMovement>().stopMoving = false;
    }
}
