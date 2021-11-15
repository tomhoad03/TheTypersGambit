using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject smallEnemy;
    public GameObject bigEnemy;
    public TMP_InputField wordField;
    public TextMeshProUGUI wordDisplay;

    private string word = "";

    void Start() {
        wordField.Select();
    }

    void OnSpacePress(InputValue spaceValue) {
        word = wordField.text;
        wordField.text = "";

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies) {
            if (enemy.GetComponent<SmallEnemyController>() != null) {
                if (enemy.GetComponent<SmallEnemyController>().word == word.ToUpper()) {
                    Destroy(enemy);
                }
            } else if (enemy.GetComponent<BigEnemyController>() != null) {
                if (enemy.GetComponent<BigEnemyController>().word == word.ToUpper()) {
                    Destroy(enemy);
                }
            }
        }
    }

    void Update() {
        wordDisplay.text = wordField.text;
        wordField.Select();
    }

    void FixedUpdate() {
        if (Random.Range(0.0f, 10000.0f) > 9950) {
            Instantiate(smallEnemy, new Vector3(-10, (float) -2.4, 0), Quaternion.identity);
        }

        if (Random.Range(0.0f, 10000.0f) > 9975) {
            Instantiate(bigEnemy, new Vector3(-10, (float) -0.75, 0), Quaternion.identity);
        }
    }
}
