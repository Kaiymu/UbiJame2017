using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [Range(1, 5)]
    public float timePlayerWait = 3f;

    public enum PlayerTeam { NONE, Team1, Team2, Team3, Team4}

    public enum Bonus { NONE, Grenade, Invincibility, InverseControl};

    [Header("Bonus prefabs")]
    public Grenade grenade;
    public Invincibility invincibility;

    [System.Serializable]
    public class BonusSprite{
        public Bonus bonus;
        public Sprite sprite;
    }

    [Header("UI")]
    public Text coutDownStart;
    public Text coutdownEndGame;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private Dictionary<PlayerTeam, List<Player>> _teamList = new Dictionary<PlayerTeam, List<Player>>();
    private Dictionary<PlayerTeam, int> _scoreList = new Dictionary<PlayerTeam, int>();

    [SerializeField]
    // Bonus
    public List<BonusSprite> spriteRendererBonusList = new List<BonusSprite>();


    public void AddPlayer(PlayerTeam playerTeam, Player player) {
        List<Player> secondPlayerList;
        _teamList.TryGetValue(playerTeam, out secondPlayerList);

        if (secondPlayerList != null)
        {
            // Useless right now, but I have the feeling it won't last long
            secondPlayerList.Add(player);
            _teamList[playerTeam] = secondPlayerList;
        }
        else {
            List<Player> playerList = new List<Player>();
            playerList.Add(player);
            _teamList.Add(playerTeam, playerList);
            _scoreList.Add(playerTeam, 0);
        }
    }

    private Timer _countDownStart;
    private Timer _timerEnd;
    private void Start()
    {
        LeanTween.init(5000);

        _countDownStart = new Timer();
        _countDownStart.Start(3);

        _timerEnd = new Timer();
        _timerEnd.Start(15f);
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

    public GameObject GetBonusFromType(Bonus bonusType)
    {
        switch (bonusType)
        {
            case Bonus.Grenade:
                return grenade.gameObject;

            case Bonus.Invincibility:
                return invincibility.gameObject;

            case Bonus.InverseControl:
                return this.gameObject;
        }

        return null;
    }

    private void Update()
    {
        EndGame();
    }

    public void EndGame()
    {
        if(_timerEnd == null || _countDownStart == null)
            return;

        _countDownStart.Tick(Time.deltaTime);

        float countDownStart = _countDownStart.GetTime();

        coutDownStart.text = countDownStart.ToString();
        if(_countDownStart.IsFinished())
        {
            _timerEnd.Tick(Time.deltaTime);
            coutdownEndGame.gameObject.SetActive(true);
            coutDownStart.transform.parent.gameObject.SetActive(false);

            foreach(var playerList in _teamList)
            {
                for(int i = 0; i < playerList.Value.Count; i++)
                {
                    var playerToInverse = playerList.Value[i];
                    playerToInverse.PlayerMovementGet.stopMoving = false;
                }
            }
        }

        coutdownEndGame.text = _timerEnd.GetTimeDecimal().ToString();
        if(_timerEnd.IsFinished())
        {
            coutdownEndGame.gameObject.SetActive(false);
            _WinTeam();
        }
    }

    private PlayerTeam _WinTeam()
    {
        int score = 0;
        PlayerTeam playerTeamWin = PlayerTeam.NONE;
        foreach(var scoring in _scoreList)
        {
            if(scoring.Value > score)
            {
                score = scoring.Value;
                playerTeamWin = scoring.Key;
            }
        }

        return playerTeamWin;
    }

    public void InverseControl(Player player)
    {
        foreach(var playerList in _teamList)
        {
            if(playerList.Key != player.playerTeam)
            {
                for(int i = 0; i < playerList.Value.Count; i++)
                {
                    var playerToInverse = playerList.Value[i];
                    playerToInverse.PlayerMovementGet.InverseMovement(true);
                    StartCoroutine(PutControlBack(playerList.Value, 5f));
                }
            }
        }
    }

    private IEnumerator PutControlBack(List<Player> playerToPutBack, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        for(int i = 0; i < playerToPutBack.Count; i++)
        {
            var playerToInverse = playerToPutBack[i];
            playerToInverse.PlayerMovementGet.InverseMovement(false);
        }
    }
}
