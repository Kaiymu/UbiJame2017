using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public GameManager.Bonus _bonus;

    private List<KeyCode> _bonusKeyList = new List<KeyCode>();

    public Grenade grenadePrefab;

    private Player _player;
    private void Start()
    {
        _player = GetComponent<Player>();
        _bonusKeyList = _player.PlayerMovementGet.useBonus;
    }

    private void Update()
    {
        if(_bonus == GameManager.Bonus.NONE)
            return;

        if(InputManager.Instance.UseAnyKey(_bonusKeyList))
        {
            Test();
        }
    }

    private void Test()
    {
        switch(_bonus)
        {
            case GameManager.Bonus.Grenade:
                var grenade =   Instantiate(grenadePrefab, transform.position, Quaternion.identity);
                grenade.UseGrenade(_player);
                _bonus = GameManager.Bonus.NONE;
            break;
        }
    }
}
