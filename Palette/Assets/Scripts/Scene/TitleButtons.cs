using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{
    public void OnClickGameStart()
    {
        LoadingScene.LoadScene("TutorialScene");
    }

    public void OnClickOption()
    {

    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
