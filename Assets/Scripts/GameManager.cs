using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _loseText;
    [SerializeField] private GameObject _restartButton;
    private void Awake()
    {
        StopGame();
        PlayerController.LoseGameAction += LoseGame;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
        _loseText.gameObject.SetActive(false);
    }
    void LoseGame()
    {
        StopGame();
        _restartButton.SetActive(true);
        _loseText.gameObject.SetActive(true);
    }
}
