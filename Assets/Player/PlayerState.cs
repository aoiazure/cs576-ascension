using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public GameManager gm;

    int current_power = -1;
    float power_timer = 0.0f;
    const float power_duration = 10.0f;

    float old_gun_damage;
    float old_player_speed;
    float old_enemy_speed;

    Gun gun;

    // Start is called before the first frame update
    void Start() {
        gun = GetComponentInChildren<Gun>();
    }

    // Update is called once per frame
    void Update() {
        // Do stuff if in a power
        if (current_power >= 0) {
            power_timer += Time.deltaTime;
            switch (current_power) {
                case 0: // damage

                    break;
                case 1: // speed

                    break;
                case 2: // slow

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
            switch (current_power) {
                case 0: // damage
                    gun.damage *= 2;
                    break;
                case 1: // speed
                    gun.firerate *= 2;
                    break;
                case 2: // slow
                    // slow enemies
                    gm.EnableSlow(true);
                    break;
                default:
                    break;
            }
        } else {
            power_timer = 0.0f;
        }
    }
}
