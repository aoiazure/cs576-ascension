using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject MainMenuUI;
    public GameObject infoUI;

    void Start()
    {
        MainMenuUI.SetActive(true);
        infoUI.SetActive(false);
    }

    public void InfoButton()
    {
        MainMenuUI.SetActive(false);
        infoUI.SetActive(true);
    }

    public void ExitButton() {
        Application.Quit();
        Debug.Log("Exited game!");
    }

    public void StartButton() {
        SceneManager.LoadScene("Ascension");
    }
}
