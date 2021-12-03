using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private Color m_oldcolor = Color.white; 

    void OnTriggerEnter (Collider other)
    {
        Renderer render = GetComponent<Renderer>();
        m_oldcolor = render.material.color;
        render.material.color = Color.green;
        Debug.Log("Player entered the zone");
    }

    void OnTriggerExit (Collider other)
    {
        Renderer render = GetComponent<Renderer>();
        m_oldcolor = render.material.color;
        render.material.color = Color.white;
        Debug.Log("Player exited the zone");
    }
}
