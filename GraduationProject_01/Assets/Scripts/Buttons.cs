using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject panel, startPanel;
    public TextMeshProUGUI text;

    public void SartAgain()
    {
        SceneManager.LoadScene(0);
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueTheGame()
    {
        if (panel.activeSelf)
            panel.gameObject.SetActive(false);
        else
            panel.gameObject.SetActive(true);

    }

    public void Information()
    {
        text.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        startPanel.gameObject.SetActive(false);
    }
}
