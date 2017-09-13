using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [Range(1, 5)]
    public float timePlayerWait = 3f;

    public enum PlayerTeam { NONE, Team1, Team2}

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private Dictionary<PlayerTeam, Player> _teamList = new Dictionary<PlayerTeam, Player>();
    private Dictionary<PlayerTeam, int> _scoreList = new Dictionary<PlayerTeam, int>();

    public void AddPlayer(PlayerTeam playerTeam, Player player) {
        _teamList.Add(playerTeam, player);
        _scoreList.Add(playerTeam, 0);
    }

    // Kill 
    public void KillPlayer(Player player) {

        if (player != null) {
            player.PlayerMovementGet.stopMoving = true;
            StartCoroutine(StopPlayerMov(player));
        }
    }

    // Movement
    private IEnumerator StopPlayerMov(Player player) {

        yield return new WaitForSeconds(timePlayerWait);
        player.PlayerMovementGet.stopMoving = false;
    }

    public void PlayerScore(Player player, bool hasStolen)
    {
        if(player == null)
            return;

        var scoreTeam = _scoreList[player.playerTeam];
        scoreTeam++;
        _scoreList[player.playerTeam] = scoreTeam;

        if(!hasStolen)
            return;

        foreach(var scoring in _scoreList)
        {
            if(scoring.Key != player.playerTeam)
            {
                _scoreList[scoring.Key] = (scoring.Value - 1);
                break;
            }
        }
    }
}
