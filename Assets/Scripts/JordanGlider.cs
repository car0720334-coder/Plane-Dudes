using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JordanEnemy : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Randomly choose left (-1) or right (1) for x
        float xDir = (Random.value < 0.5f) ? -1f : 1f;

        // Always move downward in y
        direction = new Vector2(xDir, -1f).normalized;
    }

    void Update()
    {
        // Move diagonally
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Destroy if off-screen
        if (transform.position.y <= -gameManager.verticalScreenSize * 1.25f
            || transform.position.x <= -gameManager.horizontalScreenSize * 1.25f
            || transform.position.x >= gameManager.horizontalScreenSize * 1.25f)
        {
            Destroy(gameObject);
        }
    }
}
