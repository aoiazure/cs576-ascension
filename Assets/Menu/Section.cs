using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Section : MonoBehaviour
{
    public Text rank, username, time, kills;

    public void SetText(int _rank, string _username, float _time, int _kills) {
        rank.text = _rank.ToString();
        username.text = _username;
        time.text = _time.ToString("F1");
        kills.text = _kills.ToString();
    }
}
