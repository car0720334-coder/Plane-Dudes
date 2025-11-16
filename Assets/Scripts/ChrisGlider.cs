using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrisGlider : MonoBehaviour
{
    public float speed = 5f;
    public bool goingUp;

    [Header("Zig Zag Settings")]
    public float horizontalAmplitude = 2f;   // Zig-Zag Range
    public float horizontalFrequency = 2f;   // Speed of Zig-Zag motion

    private GameManager gameManager;
    private float startX;
    private float timeOffset;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        startX = transform.position.x;

        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        if (goingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (goingUp == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        // Allows the Zig-Zag Motion to be smoother
        float newX = startX + Mathf.Sin(Time.time * horizontalFrequency + timeOffset) * horizontalAmplitude;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Destroy if off-screen
        if (transform.position.y <= -gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(gameObject);
        }
    }
}
