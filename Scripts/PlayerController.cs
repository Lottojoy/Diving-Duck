using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // สมมติว่าขอบเขตของสิ่งที่ชนมี tag ว่า "Collectible"
        if (other.CompareTag("Collectible"))
        {
            scoreManager.AddScore(1);
            scoreManager.StopCountingForSeconds(3.0f);
        }
    }
}