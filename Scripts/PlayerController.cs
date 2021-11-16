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

    public float gamePace = 5.0f;
    public bool menuOpen = true;
    public bool gameOver = false;

    private string word = "";
    private float nextSmallEnemy = 0.0f;
    private float nextBigEnemy = 0.0f;

    // Delete all enemies with the matching word
    void OnEnterWord(InputValue spaceValue) {
        word = wordField.text;
        wordField.text = "";

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies) {
            if (enemy.GetComponent<SmallEnemyController>() != null) {
                if (enemy.GetComponent<SmallEnemyController>().word.ToUpper() == word.ToUpper()) {
                    GameObject.Find("Health").GetComponent<HealthController>().health += enemy.GetComponent<SmallEnemyController>().damage / 3;
                    Destroy(enemy);
                }
            } else if (enemy.GetComponent<BigEnemyController>() != null) {
                if (enemy.GetComponent<BigEnemyController>().word.ToUpper() == word.ToUpper()) {
                    GameObject.Find("Health").GetComponent<HealthController>().health += enemy.GetComponent<BigEnemyController>().damage / 3;
                    Destroy(enemy);
                }
            }
        }
        if (word.ToUpper() == "BOULDER") {
		    Instantiate(boulder, new Vector3(11, (float) 0, 0), Quaternion.identity);
	    }
    }

    // Keep the word display updated and the word field selected
    void Update() {
        menuOpen = player.GetComponent<MenuController>().menuOpen;
        gameOver = health.GetComponent<HealthController>().gameOver;

        if (!menuOpen) {
            wordDisplay.text = wordField.text;
            wordField.Select();

            if (!gameOver) {
                if (Time.time > nextSmallEnemy) {
                    GameObject enemy = Instantiate(smallEnemy, new Vector3(-10, (float) -2.4, 0), Quaternion.identity) as GameObject;
                    enemy.transform.parent = GameObject.Find("SmallEnemies").transform;
                    nextSmallEnemy += Random.Range(2.0f, gamePace + (enemy.GetComponent<SmallEnemyController>().word.Length * 2));
                }
                if (Time.time > nextBigEnemy) {
                    GameObject enemy = Instantiate(bigEnemy, new Vector3(-10, (float) -0.75, 0), Quaternion.identity) as GameObject;
                    enemy.transform.parent = GameObject.Find("BigEnemies").transform;
                    nextBigEnemy += Random.Range(2.0f, gamePace + (enemy.GetComponent<BigEnemyController>().word.Length * 3));
                }
            }
        }
    }
}
