using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop2Bombs : MonoBehaviour
{
    public GameObject bomb1;
    public GameObject bomb2;

    private void Awake()
    {
        StartCoroutine(DropBombs());
    }

    private IEnumerator DropBombs()
    {
        bomb1.SetActive(true);

        float timer = 0;
        float interval = 1;

        while (timer < interval)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        bomb2.SetActive(true);
    }
}
