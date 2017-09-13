using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string playerName = string.Empty;

    private PlayerMovement _playerMovement;
    private PlayerColor _playerColor;
    private PlayerTrailPhysic _playerTrailPhysic;


    public PlayerMovement PlayerMovementGet {
        get { return _playerMovement; }
    }

    public PlayerColor PlayerColorGet {
        get { return _playerColor; }
    }

    public PlayerTrailPhysic PlayerTrailerPhysicGet {
        get { return _playerTrailPhysic; }
    }

    private void Awake() {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerColor = GetComponent<PlayerColor>();
        _playerTrailPhysic = GetComponent<PlayerTrailPhysic>();
    }

    private void Start() {
        GameManager.Instance.AddPlayer(this);
    }
}
