using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DragonController : MonoBehaviour
{
    public GameObject player;
    public float speed = (float) 8;
    public int damage = 10000;
    public TextMeshProUGUI paraDisplay;
    public string words = "MWAHAHA I AM THE BIG DRAGON";
    public List<string> wordsLeft;
    public List<string> wordsRight; 

    // Creates a dragon with the final paragraph
    void Start() {
        this.paraDisplay.text = words;
	    this.wordsRight = new List<string>() {"MWAHAHA", "I", "AM", "THE", "BIG", "DRAGON"};
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
    
    // See if the word entered by the user is correct
    public void enterNextWord(string word) {
        if (wordsRight[0].ToUpper() == word) {
            // Change the colour of that word in the paragraph so that the user knows they've made progress
            // example: do my <color=green>words</color> change

            // Remove the correctly typed word from the 'right' list and add it to the 'left' list
            string correctWord = wordsRight[0];
            wordsRight.RemoveAt(0);
            wordsLeft.Insert(0, correctWord);

            string left = "<color=green> ";
            string right = "<color=red> ";

            // Loop through all the words that should be green
            wordsLeft.Reverse();
            if (wordsLeft.Count != 0) {
                foreach (string w in wordsLeft) {
                                left += w;
                        left += " ";
                        }
                    }
            left += "</color>";
            wordsLeft.Reverse();
            
        
            // Loop through all the words that should be red
            foreach (string w in wordsRight) {
                right += w;
                right += " ";
            }
            right += "</color>";

            // Piece together the two parts and update the display
            paraDisplay.text = left + right;
            
            // Debugging
            // Debug.Log(left + right);

            // Check if the game has been won   
            if (wordsRight.Count == 0) {
                GameObject.Find("Player").GetComponent<PlayerController>().dragonActive = false;
                GameObject.Find("Player").GetComponent<PlayerController>().score += damage;
                GameObject.Find("Health").GetComponent<HealthController>().health += 2500;
                Destroy(GameObject.FindGameObjectsWithTag("Dragon")[0]);
            }
        }
    }
}
