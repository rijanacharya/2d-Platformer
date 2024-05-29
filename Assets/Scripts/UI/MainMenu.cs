using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingMenu;

    private void Awake()
    {
        mainMenu.SetActive(true);
        settingMenu.SetActive(false);
    }
    public void PlayGame()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }


    public void Continue()
    {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        // get last scene player was playing
        
    }
    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
    public void Setting()
    {
        //disable main menu
        mainMenu.SetActive(false);
        //enable setting menu
        settingMenu.SetActive(true);
    }
}
