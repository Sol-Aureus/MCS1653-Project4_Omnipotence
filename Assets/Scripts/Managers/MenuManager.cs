using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager menus;

    [Header("References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private AudioClip[] menuSounds;

    public bool isPaused = false;
    private bool otherMenu = false;


    // Awake is called before the first frame update
    void Awake()
    {
        // Checks if the instance is already set
        if (menus == null)
        {
            // Sets the instance to this
            menus = this;
        }
        else
        {
            // Destroys the duplicate instance
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Pauses the game if the player presses the escape key and no other menu is open
        if (Input.GetButtonDown("Cancel") && !otherMenu)
        {
            Pause();
        }
    }

    // Opens the pause menu
    public void Pause()
    {
        // Play sound effect
        //SoundFXManager.instance.PlaySound(menuSounds[0], transform, 0.8f);

        // Checks if the pause menu is already open
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Cursor.visible = isPaused;

        // Pauses the game
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    public void Win()
    {
        // Play sound effect
        SoundFXManager.instance.PlaySound(menuSounds[0], transform, 0.8f);

        // Checks if the win menu is already open
        isPaused = false;
        otherMenu = true;
        winMenu.SetActive(true);
        Cursor.visible = true;

        // Sets the stats text
        statText.text = "Enemies Killed: " + EnemySpawner.main.enemiesKilled + "\n" +
                        "Time Survived: " + Mathf.RoundToInt(EnemySpawner.main.timeSurvived) + "s";

        // Pauses the game
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    // Returns to the main menu
    public void Home()
    {
        // Play sound effect
        SoundFXManager.instance.PlaySound(menuSounds[1], transform, 0.6f);

        SceneManager.LoadScene(0);

        // Unpauses the game
        Time.timeScale = 1;
    }

    // Restarts the current level
    public void Restart()
    {
        // Play sound effect
        SoundFXManager.instance.PlaySound(menuSounds[1], transform, 0.6f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Unpauses the game
        Time.timeScale = 1;
    }
}
