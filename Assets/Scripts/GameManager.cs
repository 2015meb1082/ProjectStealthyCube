using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    #region Singleton for Game Manager
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance!=this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
