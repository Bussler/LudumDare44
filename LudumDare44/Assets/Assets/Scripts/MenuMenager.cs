using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuMenager : MonoBehaviour {

    public GameObject helpText;

    public void activateText()
    {
        if (helpText.active)
        {
            helpText.SetActive(false);
        }
        else
        {
            helpText.SetActive(true);
        }

    }

    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
