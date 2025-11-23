using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
public class Coin: MonoBehaviour
{
=======
public class Coin : MonoBehaviour
{

    public GameObject explosionPrefab;
    
>>>>>>> Stashed changes
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Player")
        {
<<<<<<< Updated upstream
            gameManager.AddScore(10);
            Destroy(this.gameObject);
        } 
=======
            gameManager.AddScore(1);
            Destroy(this.gameObject);
        }
>>>>>>> Stashed changes
    }
}
