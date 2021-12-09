using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Zone[] zones;
    Zone current_zone;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetCurrentZone(Zone z = null) {
        current_zone = z;
    }


}
