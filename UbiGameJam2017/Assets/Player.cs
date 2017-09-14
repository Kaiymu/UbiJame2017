using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string playerName = string.Empty;

    public GameManager.PlayerTeam playerTeam;

    private PlayerMovement _playerMovement;
    private PlayerColor _playerColor;
    private PlayerTrailPhysic _playerTrailPhysic;
    private PlayerInventory _playerInventory;

    public PlayerMovement PlayerMovementGet {
        get { return _playerMovement; }
    }

    public PlayerColor PlayerColorGet {
        get { return _playerColor; }
    }

    public PlayerTrailPhysic PlayerTrailerPhysicGet {
        get { return _playerTrailPhysic; }
    }

    public PlayerInventory PlayerInventoryGet
    {
        get { return _playerInventory; }
    }

    private void Awake() {

        _playerMovement = GetComponent<PlayerMovement>();
        _playerColor = GetComponent<PlayerColor>();
        _playerTrailPhysic = GetComponent<PlayerTrailPhysic>();
        _playerInventory = GetComponent<PlayerInventory>();
    }

    private void Start() {
        GameManager.Instance.AddPlayer(playerTeam, this);
    }
}
