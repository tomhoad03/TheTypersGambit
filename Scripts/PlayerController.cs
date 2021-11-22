using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject health;

    public GameObject smallEnemy;
    public GameObject bigEnemy;
    public GameObject boulder;

    public TMP_InputField wordField;
    public TextMeshProUGUI wordDisplay;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI highScoreDisplay;

    public bool menuOpen = true;
    public bool gameOver = false;
    public bool tutorialPlaying = true;

    public int score = 0;
    public int highScore = 0;

    private string word = "";
    private float gamePace = 3.0f;
    private float nextSmallEnemy = 0.0f;
    private float nextBigEnemy = 0.0f;

    // Reads a word from the user
    void OnEnterWord(InputValue spaceValue) {
        word = wordField.text;
        wordField.text = "";

        // Finds all the enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

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
                    Destroy(enemy);
                }
            }
        }

        // Activates a powerup
        if (word.ToUpper() == "BOULDER") {
		    Instantiate(boulder, new Vector3(11, (float) 0, 0), Quaternion.identity);
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
        menuOpen = player.GetComponent<MenuController>().menuOpen;
        gameOver = health.GetComponent<HealthController>().gameOver;
        tutorialPlaying = player.GetComponent<MenuController>().tutorialPlaying;

        // Keeps the word updated on the screen
        if (!menuOpen) {
            wordDisplay.text = wordField.text;
            wordField.Select();

            // Randomly generates a new enemy
            if (!gameOver && !tutorialPlaying) {
                if (Time.time > nextSmallEnemy) {
                    GameObject enemy = Instantiate(smallEnemy, new Vector3(-10, (float) -2.4, 0), Quaternion.identity) as GameObject;
                    enemy.transform.parent = GameObject.Find("SmallEnemies").transform;
                    nextSmallEnemy = Time.time + Random.Range(1.5f, gamePace + enemy.GetComponent<SmallEnemyController>().word.Length);
                }
                if (Time.time > nextBigEnemy) {
                    GameObject enemy = Instantiate(bigEnemy, new Vector3(-10, (float) -0.75, 0), Quaternion.identity) as GameObject;
                    enemy.transform.parent = GameObject.Find("BigEnemies").transform;
                    nextBigEnemy = Time.time + Random.Range(1.5f, gamePace + enemy.GetComponent<BigEnemyController>().word.Length);
                }
            }
        }
    }
}
