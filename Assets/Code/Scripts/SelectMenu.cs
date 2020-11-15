using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    Levels levels;
    GameObject mainMenu;


    private void Start()
    {
        mainMenu = GameObject.Find("MainMenu");
        gameObject.SetActive(false);
        TextAsset jsonData = (TextAsset)Resources.Load("level_data", typeof(TextAsset));
        levels = JsonUtility.FromJson<Levels>(jsonData.text);
    }

    public void loadLevel(int i)
    {
        FindObjectOfType<LevelData>().setCoordinates(levels.levels[i].level_data);
        SceneManager.LoadScene(1);
    }

    public void Back()
    {

        if (mainMenu == null)
        {
            SceneManager.LoadScene(0);
        }   
        else
        {
            gameObject.SetActive(false);
        }
            

    }

}
