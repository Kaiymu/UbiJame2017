using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public GameManager.Bonus bonus;

    private List<KeyCode> _bonusKeyList = new List<KeyCode>();

    private Player _player;

    public SpriteRenderer spriteRendererBonus;

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
        _CheckWhichBonus();
    }

    private void _CheckWhichBonus()
    {
        Sprite spriteRenderer = null;

        for(int i = 0; i < GameManager.Instance.spriteRendererBonusList.Count; i++)
        {
            var spriteRendererType = GameManager.Instance.spriteRendererBonusList[i];

            if(spriteRendererType.bonus == bonus)
            {
                spriteRenderer = spriteRendererType.sprite;
            }
        }
        
        if(spriteRenderer != null)
        {
            spriteRendererBonus.sprite = spriteRenderer;
        }
        
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
                spriteRendererBonus.sprite = null;
                bonus = GameManager.Bonus.NONE;
            break;

            case GameManager.Bonus.Invincibility:
                var invincibility = Instantiate(bonusToCreate, transform.position, Quaternion.identity).GetComponent<Invincibility>();
                invincibility.transform.parent = gameObject.transform;
                invincibility.UseInvinciblity(_player);
                spriteRendererBonus.sprite = null;
                bonus = GameManager.Bonus.NONE;
            break;

            case GameManager.Bonus.InverseControl:
            GameManager.Instance.InverseControl(_player);
            spriteRendererBonus.sprite = null;
            bonus = GameManager.Bonus.NONE;
            break;
        }
    }
}
