using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject infoUI;

    // Start is called before the first frame update
    void Start()
    {
        infoUI.SetActive(false);
    }

    public void BackButton()
    {
        MainMenuUI.SetActive(true);
        infoUI.SetActive(false);
    }

}
