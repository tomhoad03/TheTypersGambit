using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class SmallEnemyController : MonoBehaviour
{
    public float speed = (float) 30;
    public int damage = 100;
    public TextMeshProUGUI wordDisplay;
    public string word;

    // Creates an enemy with a random word
    void Start() {
        // Load the 'small' enemy words from a text file and display one at random
	string[] words = File.ReadAllLines("Assets/Words/smallWords.txt");
        word = words[Random.Range(0, words.Length)];
        damage = word.Length * 50;
        wordDisplay.text = word;
    }

    // Moves the enemy
    void FixedUpdate()
    {
        Vector3 direction = this.transform.position;
        direction.x += speed / 1000;
        this.transform.position = direction;

        if (direction.x > 9) {
            Destroy(gameObject);
            GameObject.Find("Health").GetComponent<HealthController>().health -= damage;
        }
    }
}
