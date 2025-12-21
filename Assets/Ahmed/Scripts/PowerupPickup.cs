using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    [SerializeField] private PowerupCommand command;

    [Header("Optional")]
    [SerializeField] private bool usePooling = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.CompareTag("Player")) return;

        if (command != null)
            command.Execute(other.gameObject);

      
        if (usePooling) gameObject.SetActive(false);
        else Destroy(gameObject);
    }
}
