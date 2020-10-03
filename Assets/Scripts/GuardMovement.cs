using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    [SerializeField]
    private Transform path;

    private void OnDrawGizmos()
    {
        Vector3 startPosition = path.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform wayPoint in path)
        {
            Gizmos.DrawSphere(wayPoint.position, 0.3f);
            Debug.DrawLine(previousPosition, wayPoint.position, Color.green);
            previousPosition = wayPoint.position;
        }
        Debug.DrawLine(previousPosition, startPosition, Color.green);
    }
}
