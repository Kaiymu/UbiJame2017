using UnityEngine;
using System.Collections;

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

    public int GoHorizontal()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            return 1;
        }

        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            return -1;
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