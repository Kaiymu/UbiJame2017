using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    private LeanTweenType _tweenCoinDurationType = LeanTweenType.linear;

    [Header("Parameters")]
    [Range(1, 5)]
    public int invincibilityTime;

    public void UseInvinciblity(Player player)
    {
        player.BecomeInvincible(invincibilityTime, gameObject);
    }
}
