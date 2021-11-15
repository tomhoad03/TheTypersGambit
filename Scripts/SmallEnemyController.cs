using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SmallEnemyController : MonoBehaviour
{
    public float speed = (float) 30;
    public TextMeshProUGUI wordDisplay;
    public string word;

    void Start() {
        word = "TEST";
        wordDisplay.text = word;
    }

    void FixedUpdate()
    {
        Vector3 direction = this.transform.position;
        direction.x += speed / 1000;
        this.transform.position = direction;

        if (direction.x > 9) {
            Destroy(gameObject);
        }
    }
}
