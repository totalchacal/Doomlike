using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private static Pause pause;
    public bool isPaused = false;
    public GameObject pausePanel;
    private AudioSource audioSource;
    private AudioSource sfxSource;
    public AudioClip open;
    public AudioClip close;

    private void Awake()
    {
        if (pause == null)
            pause = this;
        else
            Object.Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        sfxSource = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "MAP")
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<SC_FPSController>().isGameOver == false)
            {
                if (isPaused == false)
                {
                    Time.timeScale = 0.0f;
                    ShowPausePanel();
                    isPaused = true;
                }
                else
                {
                    Time.timeScale = 1.0f;
                    HidePausePanel();
                    isPaused = false;
                }
            }
        }
    }

    //Call this function to activate and display the Pause panel during game play
    public void ShowPausePanel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SC_FPSController>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        pausePanel.SetActive(true);
        audioSource.Pause();
        sfxSource.PlayOneShot(open);
    }

    //Call this function to deactivate and hide the Pause panel during game play
    public void HidePausePanel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SC_FPSController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
        sfxSource.PlayOneShot(close);
        audioSource.UnPause();
    }

    public void UnloadPause()
    {
        Time.timeScale = 1.0f;
        HidePausePanel();
        isPaused = false;
    }

    public void DestroyPause()
    {
        Object.Destroy(gameObject);
    }
}
