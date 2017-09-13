using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    private LeanTweenType _tweenCoinDurationType = LeanTweenType.linear;

    public Color grenadeColor;
    public GameManager.PlayerTeam grenadeTeam;

    public Player playerBound;

    public void UseGrenade(Player player)
    {
        grenadeColor = player.PlayerColorGet.color;
        grenadeTeam = player.playerTeam;
        playerBound = player;

        LeanTween.value(gameObject, _UpdateSpeedFactor, 0, 7.5f, 1f)
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
