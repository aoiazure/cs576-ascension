using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {
    private Color m_oldcolor = Color.white;

    private bool is_in_zone = false;

    void OnTriggerEnter(Collider other) {

        if(other.transform.name == "Player") {
            is_in_zone = true;

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

            // Change color
            Renderer render = GetComponent<Renderer>();
            m_oldcolor = render.material.color;
            render.material.color = Color.white;
            Debug.Log("Player exited the zone");
        }
    }
}
