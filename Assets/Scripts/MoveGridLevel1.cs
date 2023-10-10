using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGridLevel1 : MonoBehaviour
{
    private Transform gridFinalPartTransform;

    private void Awake()
    {
        gridFinalPartTransform = GameObject.FindGameObjectWithTag("GridFinalPart").transform;
        StartCoroutine(MoveGroundFinalPart());
    }

    private IEnumerator MoveGroundFinalPart()
    {
        SFXController.PlaySound("MovingGroundSound");

        float targetY = gridFinalPartTransform.position.y + 6;

        while (gridFinalPartTransform.position.y < targetY)
        {
            float newY = Mathf.MoveTowards(gridFinalPartTransform.position.y, targetY, Time.deltaTime * 5);

            gridFinalPartTransform.position = new Vector3(gridFinalPartTransform.position.x, newY, gridFinalPartTransform.position.z);

            yield return null;
        }
    }
}
