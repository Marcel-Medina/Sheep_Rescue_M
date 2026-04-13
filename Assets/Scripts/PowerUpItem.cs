using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Ensure your Hay Bale prefab has the tag "Hay"
        if (other.CompareTag("Hay"))
        {
            HayMachine machine = Object.FindFirstObjectByType<HayMachine>();

            if (machine != null)
            {
                machine.ActivateSpeedPowerUp();
            }

            Destroy(other.gameObject); // Destroy the hay
            Destroy(gameObject);      // Destroy the power-up
        }
    }
}