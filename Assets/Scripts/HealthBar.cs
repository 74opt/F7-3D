using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private static float playerHealth;
    private static RectTransform rectTransform;

    // Start is called before the first frame update
    void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        playerHealth = Player.health;

        rectTransform.localScale = new Vector3(playerHealth / 100, 1, 1);
        rectTransform.localPosition = new Vector3(playerHealth / -100 * -100 - 600, -250, 0);
    }
}
