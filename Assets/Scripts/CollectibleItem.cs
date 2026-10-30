
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleItem : MonoBehaviour
{
    public int points = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager scoreManager =
                FindFirstObjectByType<ScoreManager>();

            if (scoreManager != null)
            {
                scoreManager.AddScore(points);
            }

            Destroy(gameObject);
        }
    }
}

