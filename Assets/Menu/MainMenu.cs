using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void ExitButton() {
        Application.Quit();
        Debug.Log("Exited game!");
    }

<<<<<<< HEAD:Assets/MainMenu.cs
    public void StartButton()
    {
=======
    public void StartButton() {
>>>>>>> 3ad6980d3bb8f6905cdc6e7f84e83d7fd43090c3:Assets/Menu/MainMenu.cs
        SceneManager.LoadScene("Ascension");
    }
}
