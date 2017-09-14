using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    private LeanTweenType _tweenCoinDurationType = LeanTweenType.linear;

    [Header("Parameters")]
    [Range(0.1f, 1f)]
    public float explosionTime;
    [Range(0.1f, 10f)]
    public float sizeFactor;

    private Color _grenadeColor;
    private GameManager.PlayerTeam _grenadeTeam;

    private Player _playerLinked;

    public Player PlayerLinked
    {
        get
        {
            return _playerLinked;
        }
    }

    public Color GrenadeColor
    {
        get
        {
            return _grenadeColor;
        }
    }

    public void UseGrenade(Player player)
    {
        _grenadeColor = player.PlayerColorGet.color;
        _grenadeTeam = player.playerTeam;
        _playerLinked = player;

        LeanTween.value(gameObject, _UpdateSpeedFactor, 0, sizeFactor, explosionTime)
         .setEase(_tweenCoinDurationType)
         .setOnComplete(() => {
             Destroy(gameObject);
         });
    }

    private void _UpdateSpeedFactor(float value)
    {
        transform.localScale = new Vector3(value, value, value);
    }
}
