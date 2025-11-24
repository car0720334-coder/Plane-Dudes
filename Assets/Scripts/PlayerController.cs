using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int lives;
    private float speed;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    // Shield variables
    public bool hasShield = false;
    public GameObject shieldVisual; // Assign this in inspector - create a child GameObject with shield sprite
    public float shieldDuration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 5.0f;
        gameManager.ChangeLivesText(lives);

        //Positioning Plane at bottom of screen
        transform.position = new Vector3(0, -1.6f, 0);

        // Ensure shield is disabled at start
        if (shieldVisual != null)
            shieldVisual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        if (hasShield)
        {
            // Shield protects from one hit
            hasShield = false;
            if (shieldVisual != null)
                shieldVisual.SetActive(false);
            StopAllCoroutines(); // Stop the shield timer
            return;
        }

        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void AddALife()
    {
        if (lives > 0 && lives < 3)
        {
            lives++;   // Increase life count
            gameManager.ChangeLivesText(lives);
        }
        else if (lives == 3)
        {
            gameManager.AddScore(5);
        }
    }

    public void ActivateShield()
    {
        if (!hasShield)
        {
            hasShield = true;
            if (shieldVisual != null)
                shieldVisual.SetActive(true);
            StartCoroutine(ShieldTimer());
        }
        else
        {
            // If shield already active, add points instead
            gameManager.AddScore(10);
        }
    }

    private IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(shieldDuration);
        hasShield = false;
        if (shieldVisual != null)
            shieldVisual.SetActive(false);
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        // Calculate movement
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed;
        
        // Get screen boundaries
        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;
        
        // Calculate new position
        Vector3 newPosition = transform.position + movement;
        
        // RESTRICTIONS:
        // X: Screen wrapping
        if (newPosition.x <= -horizontalScreenSize || newPosition.x > horizontalScreenSize)
        {
            newPosition.x = newPosition.x * -1;
        }
        
        // Y: No wrapping, just hard limits
        float minY = -verticalScreenSize + 2.9f;  // Bottom of Screen invisible wall adjuster
        float maxY = 0;
        
        // If trying to go below minimum, stop at minimum
        if (newPosition.y < minY)
        {
            newPosition.y = minY;
        }
        
        // If trying to go above maximum, stop at maximum  
        if (newPosition.y > maxY)
        {
            newPosition.y = maxY;
        }
        
        transform.position = newPosition;
    }
}