using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject DoorMsg;
    [SerializeField] public GameObject SkillTreeCanvas;
    [SerializeField] public GameObject RestartCanvas;

    //bool variable whether the game is pause or not
    public static bool GameIsPaused = false;

    private GameObject player;
    private float DoorTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        DoorTimer = -1f;
    }

    private void Update()
    {
        if (DoorTimer > 0)
        {
            DoorTimer -= Time.deltaTime;
        }
        else
        {
            DoorMsg.SetActive(false);
        }
    }

    //enable skill tree canvas
    public void EnableSkillTreeCanvas()
    {
        //check whether the skill tree canvas is active
        if (SkillTreeCanvas.activeSelf)
        {
            //if it is active, disable it
            SkillTreeCanvas.SetActive(false);
            Resume();
            return;
        }
        else
        {
            SkillTreeCanvas.SetActive(true);
            Pause();
        }
    }

    //enable restart canvas
    public void EnableRestartCanvas()
    {
        RestartCanvas.SetActive(true);
        Pause();
    }

    public void EnableDoorMsg()
    {
        DoorTimer = 1f;
        if(DoorTimer > 0)
        {
            DoorMsg.SetActive(true);
        }
    }

    //pause the game
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    //resume the game
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    //quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            Destroy(Object.FindObjectsOfType<DontDestroy>()[i].gameObject);
        }
        SceneManager.LoadScene("StartMenu");
        Resume();
    }
}
