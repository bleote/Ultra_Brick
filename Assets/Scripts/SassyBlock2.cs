using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SassyBlock2 : MonoBehaviour
{
    public GameObject sassyBlockStatic2;
    public GameObject sassyBlockMoving2;
    private Transform sassyBlock2Transform;

    private void Awake()
    {
        sassyBlock2Transform = GameObject.FindGameObjectWithTag("SassyBlock2").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("Player"))
        {
            SFXController.PlaySound("SassyBlockSound");
            StartCoroutine(MoveSassyBlock2());
        }
        else
        {
            return;
        }
    }

    private IEnumerator MoveSassyBlock2()
    {
        float targetX = sassyBlock2Transform.position.x + 2;
        sassyBlockMoving2.SetActive(true);
        sassyBlockStatic2.SetActive(false);

        while (sassyBlock2Transform.position.x < targetX)
        {
            float newX = Mathf.MoveTowards(sassyBlock2Transform.position.x, targetX, Time.deltaTime * 10);

            sassyBlock2Transform.position = new Vector3(newX, sassyBlock2Transform.position.y, sassyBlock2Transform.position.z);

            yield return null;
        }

        sassyBlockStatic2.SetActive(true);
        sassyBlockMoving2.SetActive(false);
    }
}
