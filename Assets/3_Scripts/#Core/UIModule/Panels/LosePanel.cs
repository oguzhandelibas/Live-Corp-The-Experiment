using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : View
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        PlayerController.Instance.PlayerManager.SpawnPlayerCharacter();
    }
}
