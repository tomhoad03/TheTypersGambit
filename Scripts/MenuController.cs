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

    public GameObject tutorialEnemy;
    
    public bool menuOpen = true;
    public bool gameOver = false;
    public bool tutorialPlaying = false;
    public bool tutorialPlayed = false;

    private float tutorialTime = 0.0f;


    void Start()
    {
        // Hides the "replay tutorial button" on first run
        GameObject.Find("ReplayCanvas").GetComponent<Canvas>().enabled = false;

        // Determines the functionality of the menu buttons
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

    // Starts the tutorial
    void StartTutorialGame() {
        tutorialPlaying = true;
        tutorialPlayed = false;
        tutorialTime = Time.time + 5.0f;

        ShowMenu(false, true, false);

        GameObject enemy = Instantiate(tutorialEnemy, new Vector3(0, (float) -2.4, 0), Quaternion.identity) as GameObject;
        enemy.transform.parent = GameObject.Find("TutorialEnemies").transform;
    }

    void Update() {
        // Runs the tutorial
        if (tutorialPlaying && !tutorialPlayed) {
            if (Time.time > tutorialTime) {
                tutorialPlaying = false;
                tutorialPlayed = true;
                StartNormalGame();
            }
        }

        // Checks if health < 0
        gameOver = health.GetComponent<HealthController>().gameOver;
    }

    // Starts the game
    void StartNormalGame() {
        ShowMenu(false, false, false);
    }

    // Shows or hides the menu
    public void ShowMenu(bool showMenu, bool showOver, bool showUI) {
        menuOpen = showMenu;
        gameOver = showOver;

        menuBackground.GetComponent<Renderer>().enabled = showUI;
        GameObject.Find("MenuCanvas").GetComponent<Canvas>().enabled = showUI;
        GameObject.Find("ReplayCanvas").GetComponent<Canvas>().enabled = showUI;
    }
}
