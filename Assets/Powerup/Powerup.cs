using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public enum PowerType : int {
        power_damage,   // doubles damage
        power_speed,    // doubles fire rate 
        power_slow      // slows enemies by 50%
    }

    public PowerType type = PowerType.power_damage;
    public AudioClip collect_powerup;

    public void OnTriggerEnter(Collider other) {
        if(other.name == "Player") {
            // Activate Powerup
            PlayerState player_state = other.GetComponent<PlayerState>();
            player_state.ChangePower((int)type);
            // sound
            AudioSource.PlayClipAtPoint(collect_powerup, transform.position);

            Destroy(gameObject);
        }
    }
}
