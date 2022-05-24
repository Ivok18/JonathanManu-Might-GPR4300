using Might.Entity.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDiedCallback += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDiedCallback -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu(GameObject player)
    {
        gameOverMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GotToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
