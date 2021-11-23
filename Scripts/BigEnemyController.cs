using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class BigEnemyController : MonoBehaviour
{
    public float speed = (float) 20;
    public int damage = 100;
    public TextMeshProUGUI wordDisplay;
    public string word;

    // Creates an enemy with a random word
    void Start() {
        // Load the 'large' enemy words from a text file and display one at random
	    string[] words = File.ReadAllLines("Assets/Words/largeWords.txt");
        word = words[Random.Range(0, words.Length)];
        damage = word.Length * 40;
        wordDisplay.text = word;
    }

    void FixedUpdate()
    {   
        // Moves the enemy
        Vector3 direction = this.transform.position;
        direction.x += speed / 1000;
        this.transform.position = direction;

        // Damages the energy if it reaches the player
        if (direction.x > 9) {
            Destroy(gameObject);
            GameObject.Find("Health").GetComponent<HealthController>().health -= damage;
        }
    }
}
