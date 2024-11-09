using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Quit() {
        //Debug.Log("Quit");
        Application.Quit();
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public void Retry() {
        SceneManager.LoadScene(1);
    }
}
