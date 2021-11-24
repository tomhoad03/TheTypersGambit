using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{   
    public int health = 1000;
    public TextMeshProUGUI gameOverDisplay;
    public TextMeshProUGUI gameOverScoreDisplay;
    public GameObject player;

    public bool gameOver = false;
    private int maxHealth;
    private float showMenuTime;

    void Start() {
        maxHealth = health;
    }

    // Updates the health bar at the top
    void Update()
    {
        // Gets the current size of the healthbar
        Vector3 currentScale = this.transform.localScale;

        // Caps the health bar at max
        if (health >= maxHealth) {
            this.transform.localScale = new Vector3(17.0f, currentScale.y, currentScale.z);
            health = maxHealth;
        }

        // When health = 0 the game ends 
        if ((health <= 0 && !gameOver)) {
            this.transform.localScale = new Vector3(0.0f, currentScale.y, currentScale.z);

            // Deletes existing enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies) {
                Destroy(enemy);
            }

            showMenuTime = Time.time + 2.5f;
            gameOverDisplay.text = "Game Over!";

            int difficulty = player.GetComponent<PlayerController>().difficulty;

            if (difficulty % 2 == 0) {
                gameOverScoreDisplay.text = "Score (Day " + ((difficulty + 2) / 2) + "): " + player.GetComponent<PlayerController>().score;
            } else {
                gameOverScoreDisplay.text = "Score (Night " + ((difficulty + 1) / 2) + "): " + player.GetComponent<PlayerController>().score;
            }

            player.GetComponent<PlayerController>().score = 0;
            gameOver = true;
        } else if (health < maxHealth && !gameOver) {
            this.transform.localScale = new Vector3((health / (float) maxHealth) * 17, currentScale.y, currentScale.z);
        }

        // Shows the menu again after the game ends
        if (gameOver) {
            if (Time.time > showMenuTime) {
                this.transform.localScale = new Vector3(17.0f, currentScale.y, currentScale.z);
                player.GetComponent<MenuController>().ShowMenu(true, true, true);
                gameOverDisplay.text = "";
                gameOverScoreDisplay.text = "";
                health = maxHealth;
                gameOver = false;
            }
        }
    }
}
