using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{
    public void OnClickGameStart()
    {
        Manager.Sound.PlaySFX("GameStart");
        SceneManager.LoadScene("OpeningScene");
    }

    public void OnClickOption()
    {
        Manager.Sound.PlaySFX("UIClick");
    }

    public void OnClickQuit()
    {
        Manager.Sound.PlaySFX("UIClick");
        Application.Quit();
    }
}
