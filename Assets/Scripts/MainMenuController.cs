using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    public Scene MainMenu;
    public Scene GameScene;

    public int age = 12;
    private float pi = 3.14f;
    private bool amIUnderStandingThis = false;
    private string title = "hi";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
