using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string playerName = string.Empty;

    private void Start() {
        GameManager.Instance.AddPlayer(this);
    }
}
