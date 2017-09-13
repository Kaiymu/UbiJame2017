using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [Range(1, 5)]
    public float timePlayerWait = 3f;

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
        if (_playerList.Contains(player)) {
            Debug.LogError(player);
            player.PlayerMovementGet.stopMoving = true;

            StartCoroutine(StopPlayerMov(player));
        }
    }

    private IEnumerator StopPlayerMov(Player player) {

        yield return new WaitForSeconds(timePlayerWait);
        player.PlayerMovementGet.stopMoving = false;
    }

    public void PlayerScore(Player player)
    {
        if (_playerList.Contains(player))
        {
             
        }
    }
}
