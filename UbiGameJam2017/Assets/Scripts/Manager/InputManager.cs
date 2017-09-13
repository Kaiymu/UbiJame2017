using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public bool UseAnyKey(List<KeyCode> keycodeList)
    {
        for(int i = 0; i < keycodeList.Count; i++)
        {
            var leftKey = keycodeList[i];

            if(Input.GetKey(leftKey))
            {
                return true;
            }
        }

        return false;
    }

    public int GoHorizontal() {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            return 1;
        }

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            return -1;
        }

        return 0;
    }

    public int GoHorizontal(List<KeyCode> left, List<KeyCode> right)
    {
        for (int i = 0; i < left.Count; i++) {
            var leftKey = left[i];

            if (Input.GetKey(leftKey)) {
                return 1;
            }
        }

        for (int i = 0; i < right.Count; i++) {
            var rightKey = right[i];

            if (Input.GetKey(rightKey)) {
                return -1;
            }
        }

        return 0;
    }

    public int GoHorizontal(List<string> left, List<string> right) {
        for (int i = 0; i < left.Count; i++) {
            var leftKey = left[i];

            if (Input.GetKey(leftKey)) {
                return 1;
            }
        }

        for (int i = 0; i < right.Count; i++) {
            var rightKey = right[i];

            if (Input.GetKey(rightKey)) {
                return -1;
            }
        }

        return 0;
    }

    public bool IsGoingLeft()
    {
        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            return true;
        }

        return false;
    }

    public bool isGoingRight()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            return true;
        }

        return false;
    }

    public bool IsGoingHorizontal()
    {
        if(isGoingRight() || IsGoingLeft())
            return true;

        return false;
    }


    // VERTICAl
    public int GoVertical()
    {
        if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            return 1;
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            return -1;
        }

        return 0;
    }

    public int GoVertical(List<KeyCode> top, List<KeyCode> down) {
        for (int i = 0; i < top.Count; i++) {
            var leftKey = top[i];

            if (Input.GetKey(leftKey)) {
                return 1;
            }
        }

        for (int i = 0; i < down.Count; i++) {
            var rightKey = down[i];

            if (Input.GetKey(rightKey)) {
                return -1;
            }
        }

        return 0;
    }

    public bool IsGoingUp()
    {
        if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            return true;
        }
        return false;
    }

    public bool IsGoingDown()
    {
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            return true;
        }

        return false;

    }

    public bool IsGoingVertical()
    {
        if(IsGoingDown() || IsGoingUp())
            return true;

        return false;
    }

}