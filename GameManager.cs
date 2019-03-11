using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    bool gameEnded = false;

    public string currentSceneToRestart;
    public GameObject restartMenu = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        restartMenu.SetActive(false);
        Cursor.visible = false;
    }

    void Update()
    {
        if (gameEnded)
        {
            restartMenu.SetActive(true);
            if (CustomInputManager.GetDrumRed())
            {
                MainMenu();
            }
            if (CustomInputManager.GetDrumGreen())
            {
                Restart();
            }
        }
    }

    static public void GameOver()
    {
        instance.gameEnded = true;
    }

    void Restart()
    {
        SceneManager.LoadScene(currentSceneToRestart);
    }

    void MainMenu()
    {
        SceneManager.LoadScene("BrockTestMenu");
    }
}
