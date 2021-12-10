using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject MainMenuUI;
    public GameObject infoUI;
    public string input;

    void Start()
    {
        MainMenuUI.SetActive(true);
        infoUI.SetActive(false);
        PlayerPrefs.DeleteKey("username");
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

    public void readString(string s)
    {
        input = s;
        PlayerPrefs.SetString("username", input);
    }
}
