using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public static event System.Action OnPlayerWon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish")) {
            //Game won. Invoke the event
            OnPlayerWon();
        }
    }
}
