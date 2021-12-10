using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {
    
    private bool is_in_zone = false;
    
    // REFERENCES
    public GameManager gm;
    public Transform[] powerup_spawns;

    private Color m_oldcolor = Color.white;

    void OnTriggerEnter(Collider other) {
        if(other.transform.name == "Player") {
            is_in_zone = true;
            gm.SetCurrentZone(this);

            // Change color
            Renderer render = GetComponent<Renderer>();
            m_oldcolor = render.material.color;
            render.material.color = Color.green;
            Debug.Log("Player entered the zone");
        }

    }

    void OnTriggerExit(Collider other) {
        
        if(other.transform.name == "Player") {
            is_in_zone = false;
            gm.SetCurrentZone(null);

            // Change color
            Renderer render = GetComponent<Renderer>();
            m_oldcolor = render.material.color;
            render.material.color = Color.white;
            Debug.Log("Player exited the zone");
        }

    }

    // SPAWN POWERUPS
    public void SpawnPowerUp(int powerup = 0) {
        // 0 = random; 1 = ?; 2 = ?; 3 = ?
        switch (powerup) {
            
            
            case 0:
            default:
                break;
        }
    }
}
