using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            FindObjectOfType<GameTimer>().AddPoint();
            gameObject.SetActive(false);
        }
    }
}