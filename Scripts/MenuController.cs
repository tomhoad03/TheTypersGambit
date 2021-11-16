using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject health;

    public GameObject menuBackground;
    public GameObject menuHeader;
    public Button newGameButton;
    public Button tutorialButton;
    
    public bool menuOpen = true;
    public bool gameOver = false;

    private bool tutorialPlayed = false;


    void Start()
    {
        newGameButton.onClick.AddListener(() => {
            if (!tutorialPlayed) {
                StartTutorialGame();
            } else {
                StartNormalGame();
            }
        });
        tutorialButton.onClick.AddListener(() => {
            StartTutorialGame();
        });
    }

    void StartTutorialGame() {
        StartGame();
        print("Tutorial");
        StartNormalGame();
    }

    void StartNormalGame() {
        StartGame();
        print("Normal");
    }

    void StartGame() {
        tutorialPlayed = true;
        menuOpen = false;
        gameOver = false;

        menuBackground.GetComponent<Renderer>().enabled = false;
        GameObject.Find("MenuCanvas").GetComponent<Canvas>().enabled = false;
    }

    void Update() {
        gameOver = health.GetComponent<HealthController>().gameOver;
    }
}
