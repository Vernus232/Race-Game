using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InGameMenu : MonoBehaviour
{
    public static bool isPaused = false;
    [Header("Universal")]
    [SerializeField] private Text header;

    [Header("Main Menu")]
    [SerializeField] private Image background;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [Header("Options menu")]
    [SerializeField] private Button exitOptions;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        if (isPaused)
            ContinueGame();
        else
            OpenMenu();
        }
        
    }

    private void OpenMenu()
    {
        header.gameObject.SetActive(true);
        header.text = "Menu";

        background.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

        Time.timeScale = 0;

        isPaused = true;
    }

    public void ContinueGame()
    {
        TurnEverythingOff();

        Time.timeScale = 1;

        isPaused = false;
    }

    public void Options()
    {
        // Disabling main menu buttons
        continueButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        // Enable options
        header.text = "Options";
        exitOptions.gameObject.SetActive(true);
    }

    public void ExitOptions()
    {
        header.text = "Menu";

        continueButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

        exitOptions.gameObject.SetActive(false);
    }

    private void TurnEverythingOff()
    {
        header.gameObject.SetActive(false);

        continueButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        exitOptions.gameObject.SetActive(false);
    }
}
