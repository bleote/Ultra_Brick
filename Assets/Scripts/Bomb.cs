using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject poofPref;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("BlockSurprise"))
        {
            SFXController.PlaySound("ExplosionSound");
            Destroy(gameObject);
            Instantiate(poofPref, gameObject.transform.position, Quaternion.identity);
            Destroy(otherGO);
            Instantiate(poofPref, otherGO.transform.position, Quaternion.identity);
        }
        else if (otherGO.CompareTag("Player"))
        {
            SFXController.PlaySound("ExplosionSound");
            Destroy(gameObject);
            Instantiate(poofPref, gameObject.transform.position, Quaternion.identity);
        }
    }
}
