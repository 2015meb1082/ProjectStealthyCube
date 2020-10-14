using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRangeDetection : MonoBehaviour
{
    public static event System.Action OnPlayerSpotted;

    [SerializeField]
    private Light spotlight;
    [SerializeField]
    private float viewDistance;
    private float viewAngle;
    private Transform playerTransform;
    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private float timeToSpot = 0.5f;
    private float spotTimer;

    private Color originalColor;

    private void Start()
    {
        originalColor = spotlight.color;
        viewAngle = spotlight.spotAngle;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            spotTimer += Time.deltaTime;
            Color tempColor = Color.Lerp(originalColor, Color.red, spotTimer / timeToSpot);
            spotlight.color = tempColor;
        }
        else {
            spotTimer -= Time.deltaTime;
            spotlight.color = originalColor;
        }

        spotTimer = Mathf.Clamp(spotTimer, 0, timeToSpot);

        if (spotTimer >= timeToSpot) {
            //It means player is spotted, invoke the delegate
            if (OnPlayerSpotted != null) {
                OnPlayerSpotted();
            }
            
        }

    }

    private bool CanSeePlayer() {
        if (Vector3.Distance(transform.position,playerTransform.position) <= viewDistance) {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2) {

                if (!Physics.Linecast(transform.position, playerTransform.position, mask)) {
                    return true;
                }

            }
        }

        return false;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * viewDistance,Color.red);
    }
}
