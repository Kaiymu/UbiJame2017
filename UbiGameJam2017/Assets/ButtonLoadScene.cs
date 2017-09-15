using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour {

    public Button reloadMap;
    public Button backTomenu;
    public Button quit;

    void Start () {
        reloadMap.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
        backTomenu.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        quit.onClick.AddListener(() => { Application.Quit(); });
    }
	
}
