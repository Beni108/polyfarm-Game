using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame() {

        SceneManager.LoadScene("LevelSelectV2");
   
   }
    public void goMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void goEditor()
    {
        SceneManager.LoadScene("Editor");
    }
    public void exit()
    {
        Application.Quit();
    }
}
