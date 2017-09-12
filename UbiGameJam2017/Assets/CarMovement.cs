using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    public List<KeyCode> left;
    public List<KeyCode> right;
    public List<KeyCode> up;
    public List<KeyCode> down;

    public float power = 3;
    public float maxspeed = 5;
    public float turnpower = 2;
    public float friction = 3;
    public Vector2 curspeed;
    Rigidbody2D rigidbody2D;

    public bool stopMoving;

    // Use this for initialization
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (stopMoving)
            return;

        curspeed = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y);

        if (curspeed.magnitude > maxspeed) {
            curspeed = curspeed.normalized;
            curspeed *= maxspeed;
        }

       float valueVertical = InputManager.Instance.GoHorizontal(up, down);

       if (valueVertical > 0) {
            rigidbody2D.AddForce(transform.up * power);
            rigidbody2D.drag = friction;
        }
       if (valueVertical < 0) {
            rigidbody2D.AddForce(-(transform.up) * (power / 2));
            rigidbody2D.drag = friction;
        }

        float valueHorizontal = InputManager.Instance.GoHorizontal(left, right);

        if (valueHorizontal != 0) {
            
            transform.Rotate(Vector3.forward * (turnpower * valueHorizontal));
        }

        _NoGas();
    }

    void _NoGas() {
        bool gas;
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.S)) {
            gas = true;
        }
        else {
            gas = false;
        }

        if (!gas) {
            rigidbody2D.drag = friction * 2;
        }
    }
}
