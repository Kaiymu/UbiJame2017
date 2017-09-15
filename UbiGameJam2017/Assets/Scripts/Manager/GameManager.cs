using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject[] deactivateToMakeSpaceSound;

    [Range(1, 5)]
    public float timePlayerWait = 3f;

    public enum PlayerTeam { NONE, Team1, Team2, Team3, Team4 }

    public enum Bonus { NONE, Grenade, Invincibility, InverseControl };

    [Header("Bonus prefabs")]
    public Grenade grenade;
    public Invincibility invincibility;

    [System.Serializable]
    public class BonusSprite
    {
        public Bonus bonus;
        public Sprite sprite;
    }

    [System.Serializable]
    public class PlayerTeamSprite
    {
        public PlayerTeam playerTeam;
        public Texture texture;
    }

    [Header("UI")]
    public Text coutDownStart;
    public Text coutdownEndGame;
    public GameObject leaderboard;
    public GameObject parentToInstantiate;
    public ContainerPlayerStats containerPlayerStats;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this.gameObject);
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
        _timerEnd.Start(60);
    }

    private Dictionary<PlayerTeam, List<Player>> _teamList = new Dictionary<PlayerTeam, List<Player>>();
    private Dictionary<PlayerTeam, int> _scoreList = new Dictionary<PlayerTeam, int>();

    [SerializeField]
    // Bonus
    public List<BonusSprite> spriteRendererBonusList = new List<BonusSprite>();

    [SerializeField]
    // Bonus
    public List<PlayerTeamSprite> playerTextureUI = new List<PlayerTeamSprite>();


    public void AddPlayer(PlayerTeam playerTeam, Player player)
    {
        List<Player> secondPlayerList;
        _teamList.TryGetValue(playerTeam, out secondPlayerList);

        if(secondPlayerList != null)
        {
            // Useless right now, but I have the feeling it won't last long
            secondPlayerList.Add(player);
            _teamList[playerTeam] = secondPlayerList;
        } else
        {
            List<Player> playerList = new List<Player>();
            playerList.Add(player);
            _teamList.Add(playerTeam, playerList);
            _scoreList.Add(playerTeam, 0);
        }
    }


    // Kill 
    public void KillPlayer(Player player)
    {

        if(player != null)
        {
            player.PlayerMovementGet.stopMoving = true;
            StartCoroutine(StopPlayerMov(player));
        }
    }

    // Movement
    private IEnumerator StopPlayerMov(Player player)
    {

        yield return new WaitForSeconds(timePlayerWait);
        player.PlayerMovementGet.stopMoving = false;
    }

    public void PlayerScore(Player player, bool hasStolen, GameManager.PlayerTeam playerTeam)
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
            if(scoring.Key == playerTeam)
            {
                _scoreList[scoring.Key] = (scoring.Value - 1);
                break;
            }
        }
    }

    public GameObject GetBonusFromType(Bonus bonusType)
    {
        switch(bonusType)
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

    bool playerReset = false;
    public void EndGame()
    {
        if(_timerEnd == null || _countDownStart == null)
            return;

        _countDownStart.Tick(Time.deltaTime);

        float countDownStart = _countDownStart.GetTime();

        coutDownStart.text = countDownStart.ToString();
        if(_countDownStart.IsFinished() && !isGameFinished)
        {

            _timerEnd.Tick(Time.deltaTime);
            coutdownEndGame.gameObject.SetActive(true);
            coutDownStart.transform.parent.gameObject.SetActive(false);

            if(!playerReset)
            {
                playerReset = true;
                foreach(var playerList in _teamList)
                {
                    for(int i = 0; i < playerList.Value.Count; i++)
                    {
                        var playerToInverse = playerList.Value[i];
                        playerToInverse.PlayerMovementGet.stopMoving = false;
                    }
                }
            }
        }


        coutdownEndGame.text = _timerEnd.GetTimeDecimal().ToString();
        if(_timerEnd.IsFinished() && !isGameFinished)
        {
            isGameFinished = true;

            coutdownEndGame.gameObject.SetActive(false);
            foreach(var playerList in _teamList)
            {
                for(int i = 0; i < playerList.Value.Count; i++)
                {
                    var playerToInverse = playerList.Value[i];
                    playerToInverse.PlayerMovementGet.stopMoving = true;
                }
            }

            DeactivateAnnoyingPlayers();
            _WinTeam();
            _CreatePlayersLeaderboard();
        }
    }

    private void DeactivateAnnoyingPlayers()
    {
        for(int i = 0; i < deactivateToMakeSpaceSound.Length; i++)
        {
            deactivateToMakeSpaceSound[i].SetActive(false);
        }

        GetComponent<AudioSource>().enabled = false;
    }

    private bool isGameFinished = false;

    private void _CreatePlayersLeaderboard()
    {
        var list = _scoreList.Values.ToList();
        list.Sort();

        leaderboard.SetActive(true);
        int rank = 0;
        for(int i = list.Count - 1; i >= 0; i--)
        {
            var prefabUI = Instantiate(containerPlayerStats);
            prefabUI.transform.SetParent(parentToInstantiate.transform, false);

            int playerScore = list[i];

            var playerTeam = GetKey(playerScore);
            if(playerTeam == PlayerTeam.NONE)
                break;

            Color playerColor = new Color();
            for(int j = 0; j < _teamList[playerTeam].Count; j++)
            {
                playerColor = _teamList[playerTeam][j].PlayerColorGet.color;
            }
            rank++;
            string percentage = (((float)playerScore / 15200) * 100).ToString("0.00") + " %";
            prefabUI.SetInfo(playerTeam.ToString(), percentage.ToString(), RetrieveTextureFromPlayerTeam(playerTeam));
        }
    }

    private Texture RetrieveTextureFromPlayerTeam(PlayerTeam playerTeamTexture)
    {
        for(int i = 0; i < playerTextureUI.Count; i++)
        {
            var playerUI = playerTextureUI[i];
            if(playerUI.playerTeam == playerTeamTexture)
            {
                return playerUI.texture;
            }
        }

        return null;
    }

    private PlayerTeam GetKey(int score)
    {
        foreach(var scoring in _scoreList)
        {
            if(scoring.Value == score)
            {
                return scoring.Key;
            }
        }

        return PlayerTeam.NONE;
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
