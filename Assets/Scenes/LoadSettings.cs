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
    }
}
