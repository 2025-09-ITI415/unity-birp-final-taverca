using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerText;
    public Text highScore;
    public GameObject player;
    public GameObject box;

    private float startTime;
    private float elapsedTime;
    private bool isTiming = true;
    private float bestTime = Mathf.Infinity;

    void Start()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }
        startTime = Time.time;
        isTiming = true;

        UpdateHighScoreText();
    }

    void Update()
    {
        if (isTiming)
        {
            elapsedTime = Time.time - startTime;
            timerText.text = "Time: " + elapsedTime.ToString("F2");
            if (player != null && box != null)
            {
                Collider playerCollider = player.GetComponent<Collider>();
                Collider boxCollider = box.GetComponent<Collider>();

                if (playerCollider != null && boxCollider != null)
                {
                    if (playerCollider.bounds.Intersects(boxCollider.bounds))
                    {
                        StopTimer();
                    }
                }
            }
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
    }

    private void UpdateHighScoreText()
    {
        if (bestTime < Mathf.Infinity)
            highScore.text = "Best Time: " + bestTime.ToString("F2");
        else
            highScore.text = "Best Time:";
    }
    public void ResetTimer()
    {
        startTime = Time.time;
        elapsedTime = 0f;
        isTiming = true;
        timerText.text = "Time: 0.00s";
        UpdateHighScoreText();
    }
}