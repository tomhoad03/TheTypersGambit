using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialEnemyController : MonoBehaviour
{
    public float speed = (float) 0;
    public int damage = 0;
    public TextMeshProUGUI wordDisplay;
    public string word = "TEST";

     void Start() {
        wordDisplay.text = word;
    }
}
