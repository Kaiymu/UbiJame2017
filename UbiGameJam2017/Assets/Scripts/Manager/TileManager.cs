using System.Collections.Generic;
using UnityEngine;
using System;

public class TileManager : MonoBehaviour {

    public static TileManager Instance;

    private int _redColor = 0;
    private int _blueColor = 0;
    private int _neutralColor = 0;

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
 
    public Dictionary<TileCollision, Color> _dictionnaryScore = new Dictionary<TileCollision, Color>();

    public void AddTile(TileCollision tileColoration)
    {
        _dictionnaryScore.Add(tileColoration, tileColoration.TileColor);
    }

    public void SetColorForCurrentTile(TileCollision tileColoration)
    {
        foreach (var playerInfo in listPlayerInfo) {
            var listColor = playerInfo.Value;

            if (listColor.color == tileColoration.TileColor) {
            }
        }
    }
}
