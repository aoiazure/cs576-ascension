using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public enum PowerType : int {
        power_damage,
        power_speed,
        power_slow
    }

    public PowerType type = PowerType.power_damage;


    // Start is called before the first frame update
    void Start() {
        // set texture based on type maybe?
    }

    public void OnTriggerEnter(Collider other) {
        if(other.name == "Player") {
            Debug.Log("player contact with " + type);
            Destroy(gameObject);
        }
    }
}
