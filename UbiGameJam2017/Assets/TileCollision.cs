using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollision : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private Color _tileColor;

    private GameManager.PlayerTeam _tileTeam = GameManager.PlayerTeam.NONE;

    public Color TileColor
    {
        get { return _tileColor; }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        TileManager.Instance.AddTile(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player")
        {
            _RetrievePlayerInfo(other.gameObject);
        }
    }

    private void _RetrievePlayerInfo(GameObject playerObject)
    {
        var player = playerObject.GetComponent<Player>();

        _SetTileColor(player);
        _CallScoring(player);
    }

    private void _SetTileColor(Player player)
    {
        Color playerColor = player.PlayerColorGet.color;
        _tileColor = playerColor;
        _spriteRenderer.color = playerColor;
    }

    private void _CallScoring(Player player)
    {
        bool hasStolen = false;
        if(_tileTeam != GameManager.PlayerTeam.NONE)
        {
            hasStolen = true;
        }

        if(_tileTeam == player.playerTeam)
            return;

        TileManager.Instance.SetColorForCurrentTile(player, hasStolen);

        _tileTeam = player.playerTeam;
    }
}
