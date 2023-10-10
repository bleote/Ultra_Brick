using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public GameObject bomb;

    private void Awake()
    {
        bomb.SetActive(true);
    }
}
