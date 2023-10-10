using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SassyBlock : MonoBehaviour
{
    public GameObject sassyBlockStatic;
    public GameObject sassyBlockMoving;
    private Transform sassyBlockTransform;

    private void Awake()
    {
        sassyBlockTransform = GameObject.FindGameObjectWithTag("SassyBlock").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("Player"))
        {
            SFXController.PlaySound("SassyBlockSound");
            StartCoroutine(MoveSassyBlock());
        }
        else
        {
            return;
        }
    }

    private IEnumerator MoveSassyBlock()
    {
        float targetX = sassyBlockTransform.position.x + 2;
        sassyBlockMoving.SetActive(true);
        sassyBlockStatic.SetActive(false);

        while (sassyBlockTransform.position.x < targetX)
        {
            float newX = Mathf.MoveTowards(sassyBlockTransform.position.x, targetX, Time.deltaTime * 10);

            sassyBlockTransform.position = new Vector3(newX, sassyBlockTransform.position.y, sassyBlockTransform.position.z);

            yield return null;
        }

        sassyBlockStatic.SetActive(true);
        sassyBlockMoving.SetActive(false);
    }
}
