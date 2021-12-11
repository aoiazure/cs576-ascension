using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

    public GameObject board;
    public Section row_prefab;

    string current_player_name;
    float current_player_time;
    int current_player_kills;

    string name1, name2, name3, name4, name5;
    float score1, score2, score3, score4, score5;
    int kills1, kills2, kills3, kills4, kills5;

    // Start is called before the first frame update
    void Start() {
        current_player_name = PlayerPrefs.GetString("username", "Player");
        current_player_time = PlayerPrefs.GetFloat("current_time", 0.0f);
        current_player_kills = PlayerPrefs.GetInt("current_kills", 0);

        // Load Names
        name1 = PlayerPrefs.GetString("name1", "N/A");
        name2 = PlayerPrefs.GetString("name2", "N/A");
        name3 = PlayerPrefs.GetString("name3", "N/A");
        name4 = PlayerPrefs.GetString("name4", "N/A");
        name5 = PlayerPrefs.GetString("name5", "N/A");
        // Load Scores
        score1 = PlayerPrefs.GetFloat("score1", 0.6f);
        score2 = PlayerPrefs.GetFloat("score2", 0.1f);
        score3 = PlayerPrefs.GetFloat("score3", 0.2f);
        score4 = PlayerPrefs.GetFloat("score4", 0.3f);
        score5 = PlayerPrefs.GetFloat("score5", 0.4f);
        // Load Kills
        kills1 = PlayerPrefs.GetInt("kills1", 0);
        kills2 = PlayerPrefs.GetInt("kills2", 0);
        kills3 = PlayerPrefs.GetInt("kills3", 0);
        kills4 = PlayerPrefs.GetInt("kills4", 0);
        kills5 = PlayerPrefs.GetInt("kills5", 0);

        // Add and sort all high scores
        SortedList<float, ArrayList> sl = new SortedList<float, ArrayList>();
        sl.Add(-score1, new ArrayList { name1, kills1 });
        sl.Add(-score2, new ArrayList { name2, kills2 });
        sl.Add(-score3, new ArrayList { name3, kills3 });
        sl.Add(-score4, new ArrayList { name4, kills4 });
        sl.Add(-score5, new ArrayList { name5, kills5 });
        if (sl.ContainsKey(current_player_time))
            sl.Add(-current_player_time+0.01f, new ArrayList { current_player_name, current_player_kills });
        else 
            sl.Add(-current_player_time, new ArrayList { current_player_name, current_player_kills });

        // KeyValuePair<float, ArrayList>:
        // Key = TIME, n.Value[0] = NAME, n.Value[1] = KILLS
        int rank = 1;
        foreach (KeyValuePair<float, ArrayList> n in sl) {
            if (rank <= 5) {
                Debug.Log("Time: " + n.Key.ToString("F1") + ", Name: " + n.Value[0] + ", Kills: " + n.Value[1]);
                Section _p = Instantiate(row_prefab, board.transform);
                _p.SetText(rank, n.Value[0].ToString(), Mathf.Abs(n.Key), (int)n.Value[1]);

                // SAVE
                PlayerPrefs.SetString("name"+rank, n.Value[0].ToString());
                PlayerPrefs.SetFloat("score"+rank, n.Key);
                PlayerPrefs.SetInt("kills"+rank, (int)n.Value[1]);

                rank++;
            }
        }


        

    }
}
