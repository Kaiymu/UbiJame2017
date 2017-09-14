using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoTolevel : MonoBehaviour {

    public string levelName;
    public Button gotoLvl;
    // Use this for initialization
    void Start()
    {
        gotoLvl.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(levelName);
        });
    }
}
