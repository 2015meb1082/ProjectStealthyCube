using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverObject;
    [SerializeField]
    private GameObject gameWonObject;

    private void Start()
    {
        gameOverObject.SetActive(false);
        gameWonObject.SetActive(false);

        GuardRangeDetection.OnPlayerSpotted += ShowGameOverUI;
        PlayerCollisionDetection.OnPlayerWon += ShowGameWonUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.isGameOver) {
            GameManager.Instance.RestartLevel();
        }
    }

    private void ShowGameOverUI() {
        OnGameOver(gameOverObject);
    }

    private void ShowGameWonUI() {
        OnGameOver(gameWonObject);
    }
    private void OnGameOver(GameObject obj) {
        GameManager.Instance.isGameOver = true;
        obj.SetActive(true);
        GuardRangeDetection.OnPlayerSpotted -= ShowGameOverUI;
        PlayerCollisionDetection.OnPlayerWon -= ShowGameWonUI;
    }
}
