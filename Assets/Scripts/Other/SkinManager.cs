using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class SkinManager : MonoBehaviour
{
    public Image image;
    public GameObject[] Characters;
    public List<Sprite> skins = new List<Sprite>();
    public static int selectedSkin = 0;

    private void Start()
    {
        selectedSkin = 0;
        image.sprite = skins[selectedSkin];
    }

    public void NextOption()
    {
        selectedSkin++;
        if(selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        image.sprite = skins[selectedSkin];
    }

    public void BackOption() {
        selectedSkin--;
        if(selectedSkin < 0)
        {
            selectedSkin = skins.Count-1;
        }
        image.sprite = skins[selectedSkin];
    }

    public void PlayGame()
    {
        Instantiate(Characters[selectedSkin],new Vector3(0,0,0),Quaternion.identity);
        //SceneManager.LoadScene("Level01");
    }
}
