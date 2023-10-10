using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHead : MonoBehaviour
{
    private Rigidbody2D enemy;
    private float enemyInitialX;

    void Awake()
    {
        enemy = GetComponent<Rigidbody2D>();
        enemyInitialX = enemy.position.x;
    }

    void Update()
    {
        if ((enemyInitialX - enemy.position.x) > 3.99)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if ((enemyInitialX - enemy.position.x) < 0.01)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}
