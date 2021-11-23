using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject health;

    public GameObject smallEnemy;
    public GameObject bigEnemy;
    public GameObject boulder;

    public TMP_InputField wordField;
    public TextMeshProUGUI wordDisplay;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI highScoreDisplay;
    public TextMeshProUGUI boulderDisplay;
    public TextMeshProUGUI timeFreezesDisplay;
    public TextMeshProUGUI healthKitsDisplay;

    public bool menuOpen = true;
    public bool gameOver = false;
    public bool finalBossTime = false;
    public bool tutorialPlaying = true;
    public bool tutorialBouldering = false;
    public bool tutorialFreezing = false;
    public bool tutorialHealing = false;

    public int score = 0;
    public int highScore = 0;
    public int tutorialEnemiesKilled = 0;

    public int availableBoulders = 3;
    public int availableTimeFreezes = 3;
    public int availableHealthKits = 3;

    private string word = "";

    private float[] gamePaces = new float[]{6.0f, 4.5f, 6.0f, 4.0f, 6.0f, 3.5f, 6.0f, 3.0f, 6.0f, 2.5f, 100.0f};
    public int difficulty = 0;

    private float nextSmallEnemy = 0.0f;
    private float nextBigEnemy = 0.0f;

    // Reads a word from the user
    void OnEnterWord(InputValue spaceValue) {
        word = wordField.text;
        wordField.text = "";

        // Finds all the enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Blocks certain abilities during tutorial
        if (!tutorialBouldering && !tutorialFreezing) {
            foreach (GameObject enemy in enemies) {

                // Deletes enemies with a matching word
                if (enemy.GetComponent<SmallEnemyController>() != null) {
                    if (enemy.GetComponent<SmallEnemyController>().word.ToUpper() == word.ToUpper()) {
                        GameObject.Find("Health").GetComponent<HealthController>().health += enemy.GetComponent<SmallEnemyController>().damage / 4;
                        score += enemy.GetComponent<SmallEnemyController>().damage;
                        Destroy(enemy);
                    }
                } else if (enemy.GetComponent<BigEnemyController>() != null) {
                    if (enemy.GetComponent<BigEnemyController>().word.ToUpper() == word.ToUpper()) {
                        GameObject.Find("Health").GetComponent<HealthController>().health += enemy.GetComponent<BigEnemyController>().damage / 4;
                        score += enemy.GetComponent<BigEnemyController>().damage;
                        Destroy(enemy);
                    }
                } else if (enemy.GetComponent<TutorialEnemyController>() != null) {
                    if (enemy.GetComponent<TutorialEnemyController>().word.ToUpper() == word.ToUpper()) {
                        tutorialEnemiesKilled++;
                        Destroy(enemy);
                    }
                }
            }
        }


        // Activates a powerup
        if ((word.ToUpper() == "BOULDER" && !tutorialPlaying && !menuOpen && availableBoulders > 0) || (word.ToUpper() == "BOULDER" && tutorialBouldering)) {
	        // Roll a boulder across the screen right to left
	        Instantiate(boulder, new Vector3(11, (float) 0, 0), Quaternion.identity);
            availableBoulders -= 1;
	        boulderDisplay.text = "" + availableBoulders;
            tutorialBouldering = false;
	    } else if ((word.ToUpper() == "FREEZE" && !tutorialPlaying && !menuOpen && availableTimeFreezes > 0) || (word.ToUpper() == "FREEZE" && tutorialFreezing)) {
	        // Temporarily pause the location of all enemies
            availableTimeFreezes -= 1;
	        timeFreezesDisplay.text = "" + availableTimeFreezes;
            tutorialFreezing = false;
	    } else if ((word.ToUpper() == "HEALTH" && !tutorialPlaying && !menuOpen && availableHealthKits > 0) || (word.ToUpper() == "HEALTH" && tutorialHealing)) {
            // Return the player's health to full
            availableHealthKits -= 1;
	        healthKitsDisplay.text = "" + availableHealthKits;
            tutorialHealing = false;
	    }
    }

    void Update() {
        // Updates the score
        scoreDisplay.text = "Score: " + score;

        if (score > highScore && score > 0) {
            highScore = score;
            highScoreDisplay.text = "High Score: " + highScore;
        }

        // Checks if the game is over or if the menu is open
        menuOpen = this.GetComponent<MenuController>().menuOpen;
        gameOver = health.GetComponent<HealthController>().gameOver;
        tutorialPlaying = this.GetComponent<MenuController>().tutorialPlaying;

        // Enters the final boss phase of the game
        if (difficulty == 10) {
            finalBossTime = true;
        } else if (gameOver) {
            finalBossTime = false;
        }

        // Keeps the word updated on the screen
        if (!menuOpen) {
            wordDisplay.text = wordField.text;
            wordField.Select();

            // Randomly generates a new enemy
            if (!gameOver && !tutorialPlaying && !finalBossTime) {
                if (Time.time > nextSmallEnemy) {
                    GameObject enemy = Instantiate(smallEnemy, new Vector3(-10, (float) -2.4, 0), Quaternion.identity) as GameObject;
                    enemy.transform.parent = GameObject.Find("SmallEnemies").transform;
                    nextSmallEnemy = Time.time + Random.Range(1.5f, gamePaces[difficulty] + enemy.GetComponent<SmallEnemyController>().word.Length);
                }
                if (Time.time > nextBigEnemy) {
                    GameObject enemy = Instantiate(bigEnemy, new Vector3(-10, (float) -0.85, 0), Quaternion.identity) as GameObject;
                    enemy.transform.parent = GameObject.Find("BigEnemies").transform;
                    nextBigEnemy = Time.time + Random.Range(1.5f, gamePaces[difficulty] + enemy.GetComponent<BigEnemyController>().word.Length);
                }
            }
        }
    }
}
