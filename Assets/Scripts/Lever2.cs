using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever2 : MonoBehaviour
{
    public GameObject leverMiddle2;
    public GameObject leverRight2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("Player") || otherGO.CompareTag("Poop") || otherGO.CompareTag("SassyBlockBody"))
        {
            Rigidbody2D otherRb = otherGO.GetComponent<Rigidbody2D>();
            if (otherRb.velocity.x >= 0)
            {
                ActivateLever();
            }
            else
            {
                ActivateLever();
                leverRight2.transform.localScale = new Vector2(-1, 1);
            }
        }
    }

    private void ActivateLever()
    {
        SFXController.PlaySound("LeverSound");
        leverRight2.SetActive(true);
        leverMiddle2.SetActive(false);
    }
}
