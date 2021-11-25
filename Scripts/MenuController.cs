using System;
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
    public TextMeshProUGUI tutorialText;
    public Button newGameButton;
    public Button tutorialButton;

    public GameObject tutorialEnemy;

    public GameObject sky;
    
    public bool menuOpen = true;
    public bool gameOver = false;

    public bool tutorialPlaying = false;
    public bool tutorialPlayed = false;
    public bool finalBossTime = false;

    public int tutorialEnemiesKilled = 0;

    private float[] tutorialTimesA;
    private float[] tutorialTimesB;
    private float[] tutorialTimesC;
    private float[] tutorialTimesD;
    private float[] tutorialTimesE;
    private bool[] tutorialStages;

    private float[] times;
    private float startTime;

    void Start()
    {
        // Hides the "replay tutorial button" on first run
        GameObject.Find("ReplayCanvas").GetComponent<Canvas>().enabled = false;

        // Determines the functionality of the menu buttons
        newGameButton.onClick.AddListener(() => {
            if (!tutorialPlayed) {
                //StartTutorialGame();
                StartNormalGame();
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

        tutorialTimesA = new float[]{0.0f, 4.0f, 8.0f, 12.0f, 16.0f, 20.0f};

        for (int i = 0; i < tutorialTimesA.Length; i++) {
            tutorialTimesA[i] += Time.time;
        }
        tutorialStages = new bool[]{false, false, false, false, false, false, false, false, false, false};

        ShowMenu(false, true, false);
    }

    void Update() {
        // Runs the tutorial
        if (tutorialPlaying && !tutorialPlayed) {
            tutorialEnemiesKilled = this.GetComponent<PlayerController>().tutorialEnemiesKilled;

            if (!tutorialStages[0]) {
                if (Time.time > tutorialTimesA[4]) {
                    tutorialText.text = "";
                    tutorialStages[0] = true;

                    GameObject enemyA = Instantiate(tutorialEnemy, new Vector3(0, (float) -0.85, 0), Quaternion.identity) as GameObject;
                    enemyA.transform.parent = GameObject.Find("TutorialEnemies").transform;
                } else if (Time.time > tutorialTimesA[3]) {
                    tutorialText.text = "These mysterious enemies use magical shields to defend themselves! Break them.";
                } else if (Time.time > tutorialTimesA[2]) {
                    tutorialText.text = "They have sent their armies to attack us!";
                } else if (Time.time > tutorialTimesA[1]) {
                    tutorialText.text = "There are rumours of a dragon approaching from the west!";
                } else if (Time.time > tutorialTimesA[0]) {
                    tutorialText.text = "Help us hero! Our castle is under attack!";
                }
            } else if (!tutorialStages[1] && tutorialEnemiesKilled == 1) {
                tutorialTimesB = new float[]{0.0f, 4.0f, 8.0f};

                for (int i = 0; i < tutorialTimesB.Length; i++) {
                     tutorialTimesB[i] += Time.time;
                }
                tutorialStages[1] = true;
            } else if (!tutorialStages[2] && tutorialEnemiesKilled == 1) {
                if (Time.time > tutorialTimesB[2]) {
                    tutorialText.text = "";
                    tutorialStages[2] = true;
                    this.GetComponent<PlayerController>().tutorialBouldering = true;
                    this.GetComponent<PlayerController>().availableBoulders += 1;

                    GameObject enemyB = Instantiate(tutorialEnemy, new Vector3(-4, (float) -0.85, 0), Quaternion.identity) as GameObject;
                    enemyB.transform.parent = GameObject.Find("TutorialEnemies").transform;

                    GameObject enemyC = Instantiate(tutorialEnemy, new Vector3(4, (float) -0.85, 0), Quaternion.identity) as GameObject;
                    enemyC.transform.parent = GameObject.Find("TutorialEnemies").transform;

                    GameObject enemyD = Instantiate(tutorialEnemy, new Vector3(0, (float) -2.4, 0), Quaternion.identity) as GameObject;
                    enemyD.transform.parent = GameObject.Find("TutorialEnemies").transform;
                } else if (Time.time > tutorialTimesB[1]) {
                    tutorialText.text = "Watch out! Some for have arrived! Try to use a BOULDER to knock them all out!";
                } else if (Time.time > tutorialTimesB[0]) {
                    tutorialText.text = "Fantastic! Only a hero like you can read and write that well!";
                }
            } else if (!tutorialStages[3] && tutorialEnemiesKilled == 1 && this.GetComponent<PlayerController>().tutorialBouldering == false) {
                tutorialTimesC = new float[]{4.0f, 8.0f};

                for (int i = 0; i < tutorialTimesC.Length; i++) {
                     tutorialTimesC[i] += Time.time;
                }
                tutorialStages[3] = true;
            } else if (!tutorialStages[4] && tutorialEnemiesKilled == 1 && tutorialStages[3]) {
                if (Time.time > tutorialTimesC[1]) {
                    tutorialText.text = "";
                    this.GetComponent<PlayerController>().tutorialFreezing = true;
                    this.GetComponent<PlayerController>().availableTimeFreezes += 1;
                    tutorialStages[4] = true;

                    GameObject enemyE = Instantiate(tutorialEnemy, new Vector3(-8, (float) -0.85, 0), Quaternion.identity) as GameObject;
                    enemyE.transform.parent = GameObject.Find("TutorialEnemies").transform;
                    enemyE.GetComponent<TutorialEnemyController>().speed = 25;

                    GameObject enemyF = Instantiate(tutorialEnemy, new Vector3(-8, (float) -2.4, 0), Quaternion.identity) as GameObject;
                    enemyF.transform.parent = GameObject.Find("TutorialEnemies").transform;
                    enemyF.GetComponent<TutorialEnemyController>().speed = 25;
                } else if (Time.time > tutorialTimesC[0]) {
                    tutorialText.text = "Don't let up hero! You must FREEZE the incoming enemies from the west!";
                }
            } else if (!tutorialStages[5] && tutorialStages[4] && tutorialEnemiesKilled == 1 && this.GetComponent<PlayerController>().tutorialFreezing == true) {
                int count = 0;

                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                    count++;
                }

                if (count == 0) {
                    GameObject enemyE = Instantiate(tutorialEnemy, new Vector3(-8, (float) -0.85, 0), Quaternion.identity) as GameObject;
                    enemyE.transform.parent = GameObject.Find("TutorialEnemies").transform;
                    enemyE.GetComponent<TutorialEnemyController>().speed = 25;

                    GameObject enemyF = Instantiate(tutorialEnemy, new Vector3(-8, (float) -2.4, 0), Quaternion.identity) as GameObject;
                    enemyF.transform.parent = GameObject.Find("TutorialEnemies").transform;
                    enemyF.GetComponent<TutorialEnemyController>().speed = 25;
                }
            } else if (!tutorialStages[5] && tutorialStages[4] && tutorialEnemiesKilled == 1 && this.GetComponent<PlayerController>().tutorialFreezing == false) {
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                    enemy.GetComponent<TutorialEnemyController>().speed = 0;
                }
                tutorialStages[5] = true;
            } else if (!tutorialStages[6] && tutorialStages[5] && tutorialEnemiesKilled == 3) {
                tutorialTimesD = new float[]{0.0f, 4.0f};

                for (int i = 0; i < tutorialTimesD.Length; i++) {
                    tutorialTimesD[i] += Time.time;
                }
                GameObject.Find("Health").GetComponent<HealthController>().health -= 900;
                tutorialStages[6] = true;
            } else if (!tutorialStages[7] && tutorialStages[6]) {
                if (Time.time > tutorialTimesD[1]) {
                    tutorialText.text = "";
                    this.GetComponent<PlayerController>().tutorialHealing = true;
                    this.GetComponent<PlayerController>().availableHealthKits += 1;
                    tutorialStages[7] = true;
                } else if (Time.time > tutorialTimesD[0]) {
                    tutorialText.text = "You've taken some damage. You will need some more HEALTH to get back on track!";
                }
            } else if (!tutorialStages[8] && tutorialStages[7] && this.GetComponent<PlayerController>().tutorialHealing == false) {
                tutorialTimesE = new float[]{0.0f, 4.0f, 8.0f};

                for (int i = 0; i < tutorialTimesE.Length; i++) {
                    tutorialTimesE[i] += Time.time;
                }
                GameObject.Find("Health").GetComponent<HealthController>().health += 2500;
                tutorialStages[8] = true;
            } else if (!tutorialStages[9] && tutorialStages[8]) {
                if (Time.time > tutorialTimesE[2]) {
                    tutorialText.text = "";
                    tutorialStages[9] = true;
                } else if (Time.time > tutorialTimesE[1]) {
                    tutorialText.text = "Be careful hero, be smart about with your powerups and watch out for that dragon!";
                } else if (Time.time > tutorialTimesE[0]) {
                    tutorialText.text = "Phew that was tough! I fear more enemies will come, especially during the night!";
                }
            } else if (tutorialStages[9]) {
                this.GetComponent<PlayerController>().tutorialEnemiesKilled = 0;
                tutorialEnemiesKilled = 0;
                tutorialText.text = "";

                tutorialPlaying = false;
                tutorialPlayed = true;
                StartNormalGame();
            }
        } else if (!tutorialPlaying && !menuOpen && !finalBossTime) {
            // Checks if health < 0
            gameOver = health.GetComponent<HealthController>().gameOver;

            // Updates the sky - 25 * sin(x / 9.55)
            double sinAngle = Math.Sin((Time.time - startTime) / 9.55);

            Vector3 skyPosition = sky.transform.position;
            skyPosition.y = (float) sinAngle * 25;

            sky.transform.position = skyPosition;

            // Updates the difficulty based on the day or night
            if (!this.GetComponent<PlayerController>().freeze) {
                if (Time.time > times[9]) {
                    this.GetComponent<PlayerController>().difficulty = 10;
                    this.GetComponent<PlayerController>().finalBossTime = true;
                    finalBossTime = true;
                } else if (Time.time > times[8]) {
                    this.GetComponent<PlayerController>().difficulty = 9;
                } else if (Time.time > times[7]) {
                    this.GetComponent<PlayerController>().difficulty = 8;
                } else if (Time.time > times[6]) {
                    this.GetComponent<PlayerController>().difficulty = 7;
                } else if (Time.time > times[5]) {
                    this.GetComponent<PlayerController>().difficulty = 6;
                } else if (Time.time > times[4]) {
                    this.GetComponent<PlayerController>().difficulty = 5;
                } else if (Time.time > times[3]) {
                    this.GetComponent<PlayerController>().difficulty = 4;
                } else if (Time.time > times[2]) {
                    this.GetComponent<PlayerController>().difficulty = 3;
                } else if (Time.time > times[1]) {
                    this.GetComponent<PlayerController>().difficulty = 2;
                } else if (Time.time > times[0]) {
                    this.GetComponent<PlayerController>().difficulty = 1;
                } else {
                    this.GetComponent<PlayerController>().difficulty = 0;
                }
            }
        }
    }

    // Starts the game
    void StartNormalGame() {
        ShowMenu(false, false, false);

        times = new float[]{30.0f, 60.0f, 90.0f, 120.0f, 150.0f, 180.0f, 210.0f, 240.0f, 270.0f, 285.0f};

        for (int i = 0; i < times.Length; i++) {
            times[i] += Time.time;
        }
        startTime = Time.time;

        this.GetComponent<PlayerController>().availableBoulders = 3;
        this.GetComponent<PlayerController>().availableTimeFreezes = 3;
        this.GetComponent<PlayerController>().availableHealthKits = 3;
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
