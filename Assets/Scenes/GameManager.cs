using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {
    public Zone[] zones;
    Zone current_zone = null;

    public Transform[] enemy_spawn;
    public Enemy[] enemy_list;

    bool powerup_slow = false;

    /*** STATS
     * wave_number : int = current wave; game scales off of number of waves
     * time_between_waves : float = time between wave spawns
     * timer_waves : float = keeps track of time 
    ***/
    bool game_start = false;
    int wave_number = 0;
    float time_between_waves = 15.0f;
    float timer_waves = 0.0f;

    int num_enemies = 0;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // if player is actively in one
        if (game_start) {
            PlayGame();
        }


    }

    void PlayGame() {
        // Debug.Log("ACTIVE");
        if (current_zone != null) {
            int _r = Random.Range(0, enemy_list.Length);
        }
    }


    // helpers
    public void SetCurrentZone(Zone z = null) {
        current_zone = z;
        game_start = true;
    }

    public void EnableSlow(bool active) {
        powerup_slow = active;
        if (powerup_slow) { // if active, slow everything right now
            foreach (Enemy e in enemy_list) {
                NavMeshAgent a = e.GetComponent<NavMeshAgent>();
                a.speed /= 2;
            }
        } else {
            foreach (Enemy e in enemy_list) {
                NavMeshAgent a = e.GetComponent<NavMeshAgent>();
                a.speed *= 2;
            }
        }
    }

}
