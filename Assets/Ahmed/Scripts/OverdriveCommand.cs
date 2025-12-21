using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Overdrive Command")]
public class OverdriveCommand : MonoBehaviour
{
    [SerializeField] private float duration = 3f;

    public void Execute(GameObject activator)
    {
        
        var player = activator.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ActivateOverdrive(duration);
        }
    }
}
