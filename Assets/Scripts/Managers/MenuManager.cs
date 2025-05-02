using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioClip[] menuSounds;

    public bool isPaused = false;
    private bool otherMenu = false;


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

    // Returns to the main menu
    public void Home()
    {
        // Play sound effect
        //SoundFXManager.instance.PlaySound(menuSounds[1], transform, 0.8f);

        SceneManager.LoadScene(0);

        // Unpauses the game
        Time.timeScale = 1;
    }

    // Restarts the current level
    public void Restart()
    {
        // Play sound effect
        //SoundFXManager.instance.PlaySound(menuSounds[1], transform, 0.8f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Unpauses the game
        Time.timeScale = 1;
    }
}
