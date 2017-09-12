using System.Collections.Generic;
using UnityEngine;
using System;

public class TileManager : MonoBehaviour {

    public static TileManager Instance;

    private int _redColor = 0;
    private int _blueColor = 0;
    private int _neutralColor = 0;

    private List<PlayerColor> listPlayerColor = new List<PlayerColor>();

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

    public void RegisterPlayerColor(PlayerColor playerColor) {
        listPlayerColor.Add(playerColor);
    }
 
    public Dictionary<TileCollision, Color> _dictionnaryScore = new Dictionary<TileCollision, Color>();

    public void AddTile(TileCollision tileColoration)
    {
        _dictionnaryScore.Add(tileColoration, tileColoration.TileColor);
    }

    public void SetColorForCurrentTile(TileCollision tileColoration)
    {
        for(int i = 0; i < listPlayerColor.Count; i++) { 
            var listColor = listPlayerColor[i];

            Debug.LogError(listColor.color + " " + tileColoration.TileColor);
            if (listColor.color == tileColoration.TileColor) {
                Debug.LogError(listColor.color);
            }
        }
    }

}
