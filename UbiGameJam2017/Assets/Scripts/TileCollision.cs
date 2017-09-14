using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollision : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private GameManager.PlayerTeam _tileTeam = GameManager.PlayerTeam.NONE;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "TileCollider")
        {
            _RetrievePlayerInfo(other.gameObject);
        }

        if(other.tag == "Grenade")
        {
            _RetrieveColorInfo(other.gameObject);
        }
    }

    private void _RetrieveColorInfo(GameObject greade)
    {
        var grenade = greade.GetComponent<Grenade>();

        _SetTileColor(grenade.PlayerLinked.PlayerColorGet.color);
        _CallScoring(grenade.PlayerLinked, true);
    }

    private void _RetrievePlayerInfo(GameObject playerObject)
    {
        var player = playerObject.transform.parent.GetComponent<Player>();

        _SetTileColor(player.PlayerColorGet.color);
        _CallScoring(player);
    }
    private LeanTweenType _tweenCoinDurationType = LeanTweenType.linear;

    private void _SetTileColor(Color color)
    {
        StopAllCoroutines();
        LeanTween.value(gameObject, _spriteRenderer.color, color, 0.25f);
    }

    private void _CallScoring(Player player, bool isGrenade = false)
    {
        bool hasStolen = false;

        if(!isGrenade)
        {
            if(_tileTeam == player.playerTeam)
            {
                if(player.PlayerMovementGet.IsChangingFactor != 1.2f)
                {
                    player.PlayerMovementGet.SpeedChangeFactor(1.2f, 0.5f);
                }
                return;
            }

            if(!player.invincible && player.playerTeam != _tileTeam && _tileTeam != GameManager.PlayerTeam.NONE && player.PlayerMovementGet.IsChangingFactor != 0.8f)
            {
                player.PlayerMovementGet.SpeedChangeFactor(0.8f, 0.5f);
                hasStolen = true;
            }
        }

        TileManager.Instance.SetColorForCurrentTile(player, hasStolen, _tileTeam);

        _tileTeam = player.playerTeam;
    }
}
