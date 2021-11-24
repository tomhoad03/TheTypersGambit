using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialEnemyController : MonoBehaviour
{
    public float speed = (float) 0;
    public int damage = 0;
    public TextMeshProUGUI wordDisplay;
    public string word;

    void Start() {
        string[] words = {"Brute", "Enemy", "Vandal", "Thug", "Bandit", "Attacker", "Invader", "Traitor", "Villain"};
        word = words[Random.Range(0, words.Length)];
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
        }
    }
}
