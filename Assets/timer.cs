using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameTimer : MonoBehaviour
{
    public Text timerText;
    public Text highScoreText;
    public Text scoreText;
    public Button startButton;
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

    void Start()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }

        UpdateHighScoreText();
        UpdateScoreText();
        timerText.text = "Time: 0.00";
        startButton.onClick.AddListener(StartTimer);
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

        if (elapsedTime < bestTime)
        {
            bestTime = elapsedTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
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
            highScoreText.text = "Best Time:";
    }
}