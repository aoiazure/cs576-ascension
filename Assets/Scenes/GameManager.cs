using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    // Player Ref
    public GameObject player;   // Player ref

    // UI Ref
    public Text kill_tracker;   // Kill count Text UI ref
    int kill_count = 0;         // Keeps track of number of kills

    public Text game_time_text; // Game Time UI ref
    float game_timer = 0.0f;    // time elapsed in decimals
    float total_timer = 0.0f;
    int total_time_elapsed = 0; // Seconds of time elapsed

    public GameObject wave_count_text; // Wave Count UI ref

    public GameObject zone_count_text; // Number in Zone Text Ref
    public GameObject zone_timer_text; // Timer for Zone Text Ref

    // Zone Refs
    public Zone[] zones;
    Zone current_zone = null;

    // Enemy Refs
    public Enemy enemy_prefab;
    public Transform[] enemy_spawn;
    public List<Enemy> enemy_list;

    /*** STATS
     * game_start : bool = tracks if player started the game
     * wave_number : int = current wave; game scales off of number of waves
     * time_between_waves : float = time between wave spawns
     * timer_waves : float = keeps track of time of waves
     * 
     * num_enemies : int = total number of enemies spawned this wave
     * max_num_enemies : int = max number enemies for the wave = wave * 2 (min 3)
     * spawn_delay : float = delay between spawning enemies per wave. gets shorter over time
     * spawn_timer : float = keeps track of time between spawning enemies
     * 
     * num_powerups : int = powerups spawned on the map so far
     * max_num_powerups : int = max number of powerups that can exist at once
     * p_spawn_delay : float = delay between powerups spawning
     * p_spawn_timer : float = keeps track of time between spawning powerups
    ***/
    bool game_start = false;
    int wave_number = 0;
    float time_between_waves = 15.0f;
    float timer_waves = 0.0f;

    int num_enemies = 0;
    int max_num_enemies = 3; // max num enemies = wave * 2 (min 3)
    float spawn_delay = 2.0f;
    float spawn_timer = 0.0f;

    int num_powerups = 0;
    int max_num_powerups = 1;
    float p_spawn_delay = 10.0f;
    float p_spawn_timer = 0.0f;

    // Management
    /***
     * LOSE CONDITIONS:
     *  - Leaving the Zone (immediate loss)
     *  - current_zone.lose_number of enemies in the Zone (immediate loss)
     *  - An enemy in the Zone for lose_time seconds
    ***/
    bool has_lost = false;
    float lose_time = 5.0f;
    float lose_timer = 0.0f;

    // Update is called once per frame
    void Update() {
        // if player has started the game
        if (game_start && !has_lost) {
            PlayGame();
        }
    }

    // Called when game is being played
    void PlayGame() {
        // Game timer
        game_timer += Time.deltaTime;
        total_timer += Time.deltaTime;
        if (game_timer >= 1.0f) {
            game_timer = 0.0f;
            total_time_elapsed += 1;
            string minutes = (total_time_elapsed / 60).ToString(), seconds;
            if (total_time_elapsed % 60 < 10)
                seconds = "0" + (total_time_elapsed % 60);
            else
                seconds = (total_time_elapsed % 60).ToString();
            game_time_text.text = "Time:\n" + minutes + ":" + seconds;
        }

        // IF IN A ZONE
        if (current_zone != null) {
            //// WAVE TIMERS
            // tick up wave timer
            timer_waves += Time.deltaTime;
            // if enough time has passed between rounds, scale up wave
            if (timer_waves >= time_between_waves) {
                // Debug.Log("WAVE UP: " + (wave_number + 1));
                timer_waves = 0.0f;
                // tick up wave count
                wave_number += 1;
                // set max number of enemies per wave = wave * 2 (min 3)
                max_num_enemies = Mathf.Max(wave_number * 2, 3);
                // scale time between waves slowly
                time_between_waves += 2.0f;
                // scale spawn delay with wave number
                spawn_delay = (time_between_waves - 2) / max_num_enemies;
                // reset number of enemies
                num_enemies = 0;
                // Update Wave UI on screen
                StartCoroutine(UpdateWaveText());
            }

            //// ENEMY SPAWNS
            // tick up spawn timer
            spawn_timer += Time.deltaTime;
            // spawn enemy if its been long enough and we haven't reached wave cap
            if (spawn_timer >= spawn_delay && num_enemies < max_num_enemies) {
                // Debug.Log("Enemy Spawned: " + (num_enemies + 1));
                // reset spawn timer
                spawn_timer = 0.0f;
                // choose a random spawn to spawn from
                int _r = Random.Range(0, enemy_spawn.Length);
                // select that random spawn
                Transform _spawn = enemy_spawn[_r];

                // Spawn enemy at that position
                Enemy e = Instantiate(enemy_prefab, _spawn.position, Quaternion.identity);
                e.gm = this;
                e.goal = player.transform; // target player for pathfinding
                num_enemies += 1; // tick up number

                //// Wave scaling of enemies
                // increase speed slowly
                e.GetComponent<NavMeshAgent>().speed += (wave_number * 0.25f);
                e.GetComponent<NavMeshAgent>().angularSpeed += 5;
            }

            //// POWERUP SPAWNS
            p_spawn_timer += Time.deltaTime;
            if (p_spawn_timer >= p_spawn_delay && num_powerups < max_num_powerups) {
                // Debug.Log("Powerup Spawned!");
                // reset powerup spawn timer
                p_spawn_timer = 0.0f;
                num_powerups++;
                // Spawn powerup
                current_zone.SpawnPowerUp();
            }

            //// ENEMY TIMER
            // if there are enemies in the zone
            if (current_zone.enemies_in_zone > 0) {
                // Tick up lose timer
                lose_timer += Time.deltaTime;
                // Update text
                zone_count_text.GetComponent<Text>().text = "Enemies in Zone: " + current_zone.enemies_in_zone;
                zone_timer_text.GetComponent<Text>().text = "Time to Remove Enemies in Zone: " + (lose_time - lose_timer).ToString("F1");
                // Enable relevant UI
                if (!zone_count_text.activeSelf)
                    zone_count_text.SetActive(true);
                if (!zone_timer_text.activeSelf)
                    zone_timer_text.SetActive(true);
                // then if it's been too long
                if (lose_timer >= lose_time) {
                    // Lose the game
                    LoseGame("You had enemies in the zone for too long!");
                }
            } else { // If there aren't enemies or they've been removed
                // Disable texts
                if (zone_count_text.activeSelf)
                    zone_count_text.SetActive(false);
                if (zone_timer_text.activeSelf)
                    zone_timer_text.SetActive(false);
                // Reset time
                lose_timer = 0.0f;
            }

        } else {
            // Lose game if game started and then Player left zone
            LoseGame("You left the zone!");
        }
    }

    // Called when you lose the game
    public void LoseGame(string reason) {
        Debug.Log("YOU LOSE!\n" + reason);

        // Save current_time
        PlayerPrefs.SetFloat("current_time", total_timer);
        PlayerPrefs.SetInt("current_kills", kill_count);
        has_lost = true;
        Time.timeScale = 0;
        Debug.Log(total_timer);
        Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LeaderBoard");
    }


    //// Helpers
    // Show wave count
    IEnumerator UpdateWaveText() {
        Text t = wave_count_text.GetComponent<Text>();
        Color c = t.color;
        c.a = 1f;
        t.color = c;
        t.text = "Wave " + (wave_number + 1);
        wave_count_text.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f) {
            c.a = alpha;
            t.color = c;
            yield return null;
        }
        wave_count_text.SetActive(false);
    }
    // Called when an Enemy dies
    public void EnemyDie() {
        kill_count += 1;
        kill_tracker.text = "Kills: " + kill_count;
    }
    // Called when Player enters/exits a Zone
    public void SetCurrentZone(Zone z = null) {
        current_zone = z;
        game_start = true;
        // show wave number
        StartCoroutine(UpdateWaveText());
    }
    public void PowerupPickup() {
        num_powerups--;
    }
    // Enable slow Powerup across all active enemies
    public void EnableSlow(bool active) {
        if (active) { // if active, slow everything right now
            foreach (Enemy e in enemy_list) {
                NavMeshAgent a = e.GetComponent<NavMeshAgent>();
                a.speed /= 2;
            }
        } else { // if not active, undo
            foreach (Enemy _e in enemy_list) {
                NavMeshAgent a = _e.GetComponent<NavMeshAgent>();
                a.speed *= 2;
            }
        }
    }

}
