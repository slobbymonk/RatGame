using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject _completionScreen;

    public ReviewManager _reviewManager;

    public TMP_Text _timerText;

    private float _time;

    public float _timeLimit;

    public PlayerController _playerController;

    private bool _gameDone;

    public RatManager _ratManager;

    public GameObject _pauseMenu;
    private bool _pauseMenuIsActive;

    private void Awake()
    {
        instance = this;

        _completionScreen.SetActive(false);

        SetDifficulty();
    }
    void SetDifficulty()
    {
        if(PlayerPrefs.GetInt("Difficulty") == 0)
        {
            _timeLimit *= 1.2f;
        }
        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {

        }
        if (PlayerPrefs.GetInt("Difficulty") == 2)
        {
            _timeLimit *= .75f;
            _ratManager._currentSpawnDelay *= .75f;
        }
    }
    private void Update()
    {
        _time += Time.deltaTime;

        var timeLeft = _timeLimit - _time;
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (_time >= _timeLimit)
            HasCompleted();

        if(Input.GetKeyDown(KeyCode.Escape) && !_gameDone)
        {
            if (_pauseMenuIsActive)
                ClosePauseMenu();
            else
                OpenPauseMenu();
        }
    }
    public void HasCompleted()
    {
        if (!_gameDone)
        {
            _completionScreen.SetActive(true);
            _playerController.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _reviewManager.GoThroughReviews();

            _gameDone = true;
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenPauseMenu()
    {
        _pauseMenuIsActive = true;
        _pauseMenu.SetActive(true);
        _playerController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ClosePauseMenu()
    {
        _pauseMenuIsActive = false;
        _pauseMenu.SetActive(false);
        _playerController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
