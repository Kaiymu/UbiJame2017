using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMe : MonoBehaviour {

    public Button play;
    public Button quit;

    private void Start()
    {
        play.onClick.AddListener(() => { SceneManager.LoadScene("UI_ChooseLevel"); });
        quit.onClick.AddListener(() => { Application.Quit(); });
    }
}
