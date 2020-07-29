using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Scene MainMenu;
    public Scene GameScene;
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
