using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounce;
    public GameObject bouncePadUp;
    public GameObject bouncePadDown;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("Player"))
        {
            SFXController.PlaySound("BoingSound");
            otherGO.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            bouncePadUp.SetActive(true);
            bouncePadDown.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("Player"))
        {
            bouncePadUp.SetActive(false);
            bouncePadDown.SetActive(true);
        }
    }
}
