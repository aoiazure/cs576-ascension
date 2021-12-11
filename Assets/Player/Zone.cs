using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

    private bool is_in_zone = false;
    public int enemies_in_zone = 0;
    public int lose_number = 3;

    // REFERENCES
    public GameManager gm;
    public Powerup power_damage;    // doubles damage
    public Powerup power_speed;     // doubles fire rate
    public Powerup power_slow;      // slows enemies by 50%
    public Transform[] powerup_spawns;

    private Color m_oldcolor = Color.white;

    public AudioClip enter_zone;

    public void Update() {
        // Called if 
        if (enemies_in_zone >= lose_number) {
            enemies_in_zone = 0;
            gm.LoseGame("You had too many enemies enter the area!");
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.transform.name == "Player") {
            is_in_zone = true;
            gm.SetCurrentZone(this);

            // Change color
            Renderer render = GetComponent<Renderer>();
            m_oldcolor = render.material.color;
            render.material.color = Color.green;

            // play sound
            AudioSource.PlayClipAtPoint(enter_zone, transform.position);
        } else {
            // Only trigger enemies if player is in it
            if (is_in_zone) { 
                Enemy e = other.GetComponent<Enemy>();
                if (e != null) {
                    Debug.Log("ENEMY ENTERED");
                    e.zone = this;
                    enemies_in_zone++;
                }
            }
        }

    }

    void OnTriggerExit(Collider other) {
        if (other.transform.name == "Player") {
            is_in_zone = false;
            gm.SetCurrentZone(null);

            // Change color
            Renderer render = GetComponent<Renderer>();
            m_oldcolor = render.material.color;
            render.material.color = Color.white;
        } else {
            Debug.Log("SOMETHING LEFT");
            if (is_in_zone) {
                Enemy e = other.GetComponent<Enemy>();
                if (e != null) {
                    Debug.Log("ENEMY LEFT");
                    e.zone = null;
                    enemies_in_zone--;
                }
            }
        }
    }

    public void EnemyInZoneDie() {
        Debug.Log("DEATH");
        enemies_in_zone--;
    }

    // SPAWN POWERUPS
    public void SpawnPowerUp(int powerup = 3) {
        // 0 = ?; 1 = ?; 2 = ?; 3 = random
        Powerup p;
        int _p = powerup;
        if (_p == 3) {
            _p = Random.Range(0, 3);

        }
        int _r = Random.Range(0, powerup_spawns.Length);
        Transform _spawn = powerup_spawns[_r];
        switch (_p) {
            case 0:
                p = Instantiate(power_damage, _spawn.position, Quaternion.identity);
                break;
            case 1:
                p = Instantiate(power_speed, _spawn.position, Quaternion.identity);
                break;
            case 2:
                p = Instantiate(power_slow, _spawn.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
