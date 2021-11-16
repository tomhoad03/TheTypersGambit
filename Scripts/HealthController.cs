using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    public int health = 1000;
    public bool gameOver = false;
    public TextMeshProUGUI gameOverDisplay;

    private int maxHealth;

    void Start() {
        maxHealth = health;
    }

    // Updates the health bar at the top
    void Update()
    {
        Vector3 currentScale = this.transform.localScale;

        if (health > maxHealth) {
            this.transform.localScale = new Vector3(17.0f, currentScale.y, currentScale.z);
            health = maxHealth;
        }

        if (health <= 0 && !gameOver) {
            this.transform.localScale = new Vector3(0.0f, currentScale.y, currentScale.z);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies) {
                Destroy(enemy);
            }

            gameOverDisplay.text = "Game Over!";
            gameOver = true;
        } else if (health < maxHealth && !gameOver) {
            this.transform.localScale = new Vector3((health / (float) maxHealth) * 17, currentScale.y, currentScale.z);
        }
    }
}
