using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;
    public GameObject Door;


    // Update is called once per frame
    void Update()
    {
        if (Door.activeSelf==true)
        {
            //Debug.Log(1);
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 7)
        {
            for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
            {
                Destroy(Object.FindObjectsOfType<DontDestroy>()[i].gameObject);
            }
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        
        yield return new  WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
