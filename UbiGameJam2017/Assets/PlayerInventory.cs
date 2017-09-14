using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public GameManager.Bonus bonus;

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
        if(bonus == GameManager.Bonus.NONE)
            return;

        if(InputManager.Instance.UseAnyKey(_bonusKeyList))
        {
            _UseBonus();
        }
    }

    public void SetCurrentBonus(GameManager.Bonus bonusSet)
    {
        // If we already have an item, we don't care
        if (bonus != GameManager.Bonus.NONE)
            return;

        bonus = bonusSet;
    }

    private void _UseBonus()
    {
        var bonusToCreate = GameManager.Instance.GetBonusFromType(bonus);

        if (bonusToCreate == null)
            return;

        switch (bonus)
        {
            case GameManager.Bonus.Grenade:
                var grenade = Instantiate(bonusToCreate, transform.position, Quaternion.identity).GetComponent<Grenade>();
                grenade.UseGrenade(_player);
                bonus = GameManager.Bonus.NONE;
            break;
        }
    }
}
