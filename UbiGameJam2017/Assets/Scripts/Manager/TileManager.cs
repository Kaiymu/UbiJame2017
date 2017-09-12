using System.Collections.Generic;
using UnityEngine;
using System;

public class TileManager : MonoBehaviour {

    public static TileManager Instance;

    private int _redColor = 0;
    private int _blueColor = 0;
    private int _neutralColor = 0;

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

 
    public Dictionary<TileCollision, Color> _dictionnaryScore = new Dictionary<TileCollision, Color>();

    public void AddTile(TileCollision tileColoration)
    {
        _dictionnaryScore.Add(tileColoration, tileColoration.TileColor);
    }

    public void SetColorForCurrentTile(TileCollision tileColoration)
    {
        return;
        _redColor = 0;
        _blueColor = 0;
        _neutralColor = 0;

        if(_dictionnaryScore.ContainsKey(tileColoration))
        {
            _dictionnaryScore[tileColoration] = tileColoration.TileColor;
        }

        foreach(var tileColor in _dictionnaryScore)
        {
            if(tileColor.Value == Color.red)
            {
                _redColor++;
            } else if(tileColor.Value == Color.blue)
            {
                _blueColor++;
            } else
            {
                _neutralColor = 0;
            }
        }
    }

}
