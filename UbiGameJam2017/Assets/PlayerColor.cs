using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {

    public Color color = Color.red;

    public void Start() {
        TileManager.Instance.RegisterPlayerColor(this.name, this);
    }
}
