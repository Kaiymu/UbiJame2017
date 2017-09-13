using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    public List<KeyCode> left;
    public List<KeyCode> right;
    public List<KeyCode> up;
    public List<KeyCode> down;
    public List<KeyCode> useSpeed;
    public List<KeyCode> useSpell;

    public float power = 3;
    public float maxspeed = 5;
    public float turnpower = 2;
    public float friction = 3;
    public Vector2 curspeed;
    Rigidbody2D rigidbody2D;

    public bool stopMoving;
    public float speedFactor = 1;

    private bool _alreadyUsed = false;
    private float _isChangingFactor = 0f;

    public float IsChangingFactor
    {
        get
        {
            return _isChangingFactor;

        }
    }

    // Use this for initialization
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (stopMoving)
            return;

        rigidbody2D.AddForce(transform.up * power * speedFactor);
        rigidbody2D.drag = friction;

        float valueHorizontal = InputManager.Instance.GoHorizontal(left, right);

        if (valueHorizontal != 0) {
            transform.Rotate(Vector3.forward * (turnpower * valueHorizontal) * speedFactor);
        }

        _NoGas();
    }

    private void _NoGas() {
        bool gas;
        float valueVertical = InputManager.Instance.GoHorizontal(up, down);
        if (valueVertical != 0) {
            gas = true;
        }
        else {
            gas = false;
        }

        if (!gas) {
            rigidbody2D.drag = friction * 2;
        }
    }

    // Player

    private LeanTweenType _tweenCoinDurationType = LeanTweenType.linear;

    public void SpeedChangeFactor(float factorChange, float waitTime, UnityAction onComplete = null)
    {
        StopAllCoroutines();
        _isChangingFactor = factorChange;

        LeanTween.value(gameObject, UpdateSpeedFactor, speedFactor, factorChange, 0.3f)
            .setEase(_tweenCoinDurationType)
            .setOnComplete(() => {
                StartCoroutine(_ResetToSpeedBaseValue(waitTime, onComplete));
            });
    }

    public void UpdateSpeedFactor(float val)
    {
        speedFactor = val; 
    }

    private IEnumerator _ResetToSpeedBaseValue(float waitTime, UnityAction onComplete = null)
    {
        yield return new WaitForSeconds(waitTime);

        speedFactor = 1f;
        if(onComplete != null)
        {
            onComplete();
        }
    }
}
