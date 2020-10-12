using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRangeDetection : MonoBehaviour
{
    [SerializeField]
    private Light spotlight;
    [SerializeField]
    private float viewDistance;
    private float viewAngle;
    private Transform playerTransform;
    [SerializeField]
    private LayerMask mask;

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
            spotlight.color = Color.red;
        }
        else {
            spotlight.color = originalColor;
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
