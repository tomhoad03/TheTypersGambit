using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class BoulderController : MonoBehaviour
{
    public float speed = (float) 69;
    public TextMeshProUGUI wordDisplay; // displays "Boulder Acitivated!" or somethin
   
    void Start()
    {	
	
    }

    // Roll the boulder across the screen, right to left taking out enemies in its path
    void Update()
    {
	Vector3 direction = this.transform.position;
        direction.x += speed / 1000;
        this.transform.position = direction;

	// If the boulder has crushed an enemy, destroy it
	GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies) {
		if (direction.x > enemy.transform.position.x) {
			Destroy(enemy);
		}
        }
    }
}
