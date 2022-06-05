using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    private static TMP_Text textMesh;

    // Start is called before the first frame update
    void Start() {
        textMesh = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update() {
        int seconds = (int) Time.time;
        int minutes = seconds / 60;
        seconds = seconds % 60;

        if (Player.health <= 0) {
            textMesh.text = "lmao loser you only survived for " + minutes + ":" + seconds;
        } else {
            textMesh.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
