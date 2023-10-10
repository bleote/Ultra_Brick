using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject leverMiddle;
    public GameObject leverRight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("Player") || otherGO.CompareTag("Poop"))
        {
            Rigidbody2D otherRb = otherGO.GetComponent<Rigidbody2D>();
            if (otherRb.velocity.x >= 0)
            {
                ActivateLever();
            }
            else
            {
                ActivateLever();
                leverRight.transform.localScale = new Vector2(-1, 1);
            }
        }
    }

    private void ActivateLever()
    {
        SFXController.PlaySound("LeverSound");
        leverRight.SetActive(true);
        leverMiddle.SetActive(false);
    }
}
