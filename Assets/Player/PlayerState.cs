using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour {
    // Refs
    public GameManager gm;
    public GameObject powerup_time_text;

    // Powers
    int current_power = -1;
    float power_timer = 0.0f;
    const float power_duration = 10.0f;


    Gun gun;

    // Start is called before the first frame update
    void Start() {
        gun = GetComponentInChildren<Gun>();
        powerup_time_text.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        // Do stuff if in a power
        if (current_power >= 0) {
            power_timer += Time.deltaTime;
            // Adjust timer UI
            Text t = powerup_time_text.GetComponent<Text>();
            switch (current_power) {
                case 0: // damage
                    t.text = "Damage Powerup: " + (10.0f - power_timer).ToString("F1") + "s";
                    break;
                case 1: // speed
                    t.text = "Firerate Powerup: " + (10.0f - power_timer).ToString("F1") + "s";
                    break;
                case 2: // slow
                    t.text = "Slow Powerup: " + (10.0f - power_timer).ToString("F1") + "s";
                    break;
                default:
                    break;
            }

            // if power runs out
            if (power_timer >= power_duration) {
                // undo power changes
                switch (current_power) {
                    case 0: // damage
                        gun.damage /= 2;
                        break;
                    case 1: // speed
                        gun.firerate /= 2;
                        break;
                    case 2: // slow
                        gm.EnableSlow(false);
                        break;
                    default:
                        break;
                }
                current_power = -1; // reset to no power
                power_timer = 0.0f; // reset timer
                powerup_time_text.SetActive(false);
            }

        }
    }

    public void ChangePower(int power) {
        Debug.Log((Powerup.PowerType)power);
        // if does not already have or already have one
        if (power != current_power) {
            // picking up new power, so reset current's effects
            switch (current_power) {
                case 0: // damage
                    gun.damage /= 2;
                    break;
                case 1: // speed
                    gun.firerate /= 2;
                    break;
                case 2: // slow
                    gm.EnableSlow(false);
                    break;
                default:
                    // NO power at the moment so do nothing
                    break;
            }
            // apply new power
            current_power = power;
            power_timer = 0.0f;
            Text t = powerup_time_text.GetComponent<Text>();
            switch (current_power) {
                case 0: // damage
                    t.text = "Damage Powerup: " + (10.0f - power_timer);
                    gun.damage *= 2;
                    break;
                case 1: // speed
                    t.text = "Firerate Powerup: " + (10.0f - power_timer);
                    gun.firerate *= 2;
                    break;
                case 2: // slow
                    t.text = "Slow Powerup: " + (10.0f - power_timer);
                    // slow enemies
                    gm.EnableSlow(true);
                    break;
                default:
                    break;
            }
            powerup_time_text.SetActive(true);
        } else {
            power_timer = 0.0f;
        }
    }
}
