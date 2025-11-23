using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject shieldPrefab;

    public AudioSource audioSource;
    public AudioClip powerupSound;
    public AudioClip powerdownSound;

    public bool isShieldActive = false;

    // Call this to turn shield ON
    public void ActivateShield()
    {
        isShieldActive = true;
        shieldPrefab.SetActive(true);

        if (powerupSound != null)
            audioSource.PlayOneShot(powerupSound);
    }

    // Call this to turn shield OFF
    public void DeactivateShield()
    {
        isShieldActive = false;
        shieldPrefab.SetActive(false);

        if (powerdownSound != null)
            audioSource.PlayOneShot(powerdownSound);
    }
}
