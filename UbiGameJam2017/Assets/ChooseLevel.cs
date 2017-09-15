using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{

    public Button oneVsOne;
    public Button twoVsTwo;
    public Button fourVsFour;

    private void Start()
    {
        oneVsOne.onClick.AddListener(() => { SceneManager.LoadScene("1v1_tutoriel"); });
        twoVsTwo.onClick.AddListener(() => { SceneManager.LoadScene("2v2_tutoriel"); });
        fourVsFour.onClick.AddListener(() => { SceneManager.LoadScene("1v1v1v1_tutoriel"); });
    }
}
