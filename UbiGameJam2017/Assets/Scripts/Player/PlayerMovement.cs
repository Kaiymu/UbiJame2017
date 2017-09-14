using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    public List<KeyCode> left;
    public List<KeyCode> right;
    public List<KeyCode> up;
    public List<KeyCode> down;
    public List<KeyCode> useBonus;

    public float power = 3;
    public float turnpower = 2;
    public float friction = 3;
    Rigidbody2D rigidbody2D;

    public bool stopMoving;
    public float speedFactor = 1;

    private float _isChangingFactor = 0f;

    public SpriteRenderer malusRenderer;

    public bool inverseMovement;


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

        float valueHorizontal;

        if(!inverseMovement)
        {
            valueHorizontal = InputManager.Instance.GoHorizontal(left, right);
        } else
        {
            valueHorizontal = InputManager.Instance.GoHorizontal(right, left);
        }

        if (valueHorizontal != 0) {
            transform.Rotate(Vector3.forward * (turnpower * valueHorizontal) * speedFactor);
        }

        _NoGas();
    }

    private void _NoGas() {
        bool gas;
        float valueVertical;

        if(!inverseMovement)
        {
            valueVertical = InputManager.Instance.GoHorizontal(up, down);
        } else
        {
            valueVertical = InputManager.Instance.GoHorizontal(down, up);
        }

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

    public void InverseMovement(bool inverse)
    {
        malusRenderer.gameObject.SetActive(inverse);
        inverseMovement = inverse;
    }
}
