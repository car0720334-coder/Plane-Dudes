using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float speed = 3f;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make the power-up move downward
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destroy if off-screen at the bottom
        // Use a larger threshold since it spawns at the top
        if (transform.position.y < -gameManager.verticalScreenSize * 1.2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().ActivateShield();
            Destroy(this.gameObject);
        } 
    }
}