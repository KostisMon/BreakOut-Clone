using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour {

    public Button startGameButton, creditsButton, quitButton, backButton ;
    public AudioClip buttonPressed;
    public GameObject panel;
    private AudioSource audioSrc;
    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        //disables the info panel
        panel.SetActive(false);
        backButton.gameObject.SetActive(false);         // disables the back button

        //making listeres for each button of the ui
        if (startGameButton)
        {
            startGameButton.onClick.AddListener(() => { StartGame(); });
        }
        if (quitButton)
        {
            quitButton.onClick.AddListener(() => { QuitGame(); });
        }
        if (creditsButton)
        {
            creditsButton.onClick.AddListener(() => { Credits(); });
        }
        if (backButton)
        {
            backButton.onClick.AddListener(() => { GoBack(); });
        }
    }
    // enabling and disabling ui elements when bacck button is pressed
    private void GoBack()
    {
        backButton.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        panel.SetActive(false);
    }

    // doees the same with credits
    private void Credits()
    {
        backButton.gameObject.SetActive(true);
        startGameButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        panel.SetActive(true);
        
    }

    private void QuitGame()
    {

        Application.Quit();

    }

    private void StartGame()
    {

        SceneManager.LoadScene(1);

    }

    public void ButtonClicked()
    {
        audioSrc.PlayOneShot(buttonPressed);
    }
}
