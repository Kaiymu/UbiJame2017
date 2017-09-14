using System.Collections.Generic;
using UnityEngine;
using System;

public class TileManager : MonoBehaviour {

    public static TileManager Instance;

    private Color _neutralColor;

    private Dictionary<string, PlayerColor> listPlayerInfo = new Dictionary<string, PlayerColor>();

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

    public void RegisterPlayerColor(string playerName, PlayerColor playerColor) {
        listPlayerInfo.Add(playerName, playerColor);
    }


    public void SetColorForCurrentTile(Player player, bool hasStolen, GameManager.PlayerTeam playerTeam)
    {
        GameManager.Instance.PlayerScore(player, hasStolen, playerTeam);
    }
}
