using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public Text timerText;
    public Text highScoreText;
    public Text scoreText;
    public Text finalScore;
    public Button startButton;
    public Button restartButton;
    public GameObject player;
    public GameObject winner;
    public GameObject intro;
    public List<GameObject> collectibles;
    public List<GameObject> doors;

    private float startTime;
    private float elapsedTime;
    private bool isTiming = false;

    private int score = 0;
    private const int maxScore = 12;

    private float bestTime = Mathf.Infinity;
    private const string BEST_TIME = "BestTime";

    void Start()
    {
        if (PlayerPrefs.HasKey(BEST_TIME))
        {
            bestTime = PlayerPrefs.GetFloat(BEST_TIME);
        }
        else
        {
            bestTime = Mathf.Infinity;
        }

            UpdateHighScoreText();
        UpdateScoreText();
        timerText.text = "Time: 0.00";
        startButton.onClick.AddListener(StartTimer);
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    void Update()
    {
        if (!isTiming) return;

        elapsedTime = Time.time - startTime;
        timerText.text = "Time: " + elapsedTime.ToString("F2");
    }
    public void StartTimer()
    {
        score = 0;
        UpdateScoreText();

        startTime = Time.time;
        elapsedTime = 0f;
        isTiming = true;
        startButton.interactable = false;
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
            intro.SetActive(false);
        }
    }

    public void StopTimer()
    {
        if (!isTiming) return;

        isTiming = false;
        finalScore.text = "Your Final Time: " + elapsedTime.ToString("F2");

        if (elapsedTime < bestTime)
        {
            bestTime = elapsedTime;
            PlayerPrefs.SetFloat(BEST_TIME, bestTime);
            PlayerPrefs.Save();
        }

        UpdateHighScoreText();
        winner.SetActive(true);
    }

    public void AddPoint()
    {
        if (!isTiming) return;

        score++;
        UpdateScoreText();

        if (score >= maxScore)
        {
            StopTimer();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score + " / " + maxScore;
    }

    private void UpdateHighScoreText()
    {
        if (bestTime < Mathf.Infinity)
            highScoreText.text = "Best Time: " + bestTime.ToString("F2");
        else
            highScoreText.text = "Best Time: --";
    }
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
