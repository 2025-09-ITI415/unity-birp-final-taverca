using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool collected = false;

    private void OnMouseDown()
    {
        if (collected) return;

        collected = true;

        GameTimer timer = FindObjectOfType<GameTimer>();
        if (timer != null)
        {
            timer.AddPoint();
        }

        gameObject.SetActive(false);
    }
}
