using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour {

    // PLAYER PREFS
    public string player_name = "Player";
    // float volume;
    public float mouse_sensitivity = 1.0f;


    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("player_name")) {
            player_name = PlayerPrefs.GetString("player_name");
        } else {
            PlayerPrefs.SetString("player_name", "Player");
            player_name = PlayerPrefs.GetString("player_name");
        }

        //if (PlayerPrefs.HasKey("volume")) {
        //    volume = PlayerPrefs.GetFloat("volume");
        //} else {
        //    PlayerPrefs.SetFloat("volume", 1.0f);
        //    volume = PlayerPrefs.GetFloat("volume");
        //}

        if (PlayerPrefs.HasKey("mouse_sensitivity")) {
            mouse_sensitivity = PlayerPrefs.GetFloat("mouse_sensitivity");
        } else {
            PlayerPrefs.SetFloat("mouse_sensitivity", 1.0f);
            mouse_sensitivity = PlayerPrefs.GetFloat("mouse_sensitivity");
        }
    }
}
