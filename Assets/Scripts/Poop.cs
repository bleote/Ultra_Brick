using TMPro;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public GameObject poofPref;
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("Player") || otherGO.CompareTag("Cake") || otherGO.CompareTag("SassyBlockRange"))
        {
            return;
        }
        else if (otherGO.CompareTag("Enemy"))
        {
            SFXController.PlaySound("PoopSplashSound");
            Destroy(gameObject);
            Instantiate(poofPref, otherGO.transform.position, Quaternion.identity);
            SFXController.PlaySound("UghSound");
            Destroy(otherGO);
            StaticValues.scorePoints += 50;
            scoreText.text = "Score: " + StaticValues.scorePoints;
        }
        else
        {
            SFXController.PlaySound("PoopSplashSound");
            Destroy(gameObject);
        }
    }
}
