using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollision : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private Color _tileColor;

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
       if(other.GetComponent<Collider2D>().tag == "Player")
        {
            _RetrievePlayerInfo(other.gameObject);
        }
    }

    private void _RetrievePlayerInfo(GameObject player)
    {
        Color playerColor = player.GetComponent<PlayerColor>().color;
        _tileColor = playerColor;
        _spriteRenderer.color = playerColor;
        TileManager.Instance.SetColorForCurrentTile(this);
    }
}
