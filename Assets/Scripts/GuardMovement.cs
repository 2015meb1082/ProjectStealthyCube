using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    [SerializeField]
    private Transform path;
    [SerializeField]
    private float moveSpeed = 8.0f;
    [SerializeField]
    private float waitingTime = 2.0f;
    [SerializeField]
    private float turnSpeed = 100f;
    private void Start()
    {
        Vector3[] wayPoints = new Vector3[path.childCount];
        for (int i=0;i<path.childCount;i++) {
            wayPoints[i] = path.GetChild(i).position;
            wayPoints[i] = new Vector3(wayPoints[i].x,transform.position.y,wayPoints[i].z);
        }
        StartCoroutine(FollowPath(wayPoints));
    }

    //Follow Path Coroutine
    IEnumerator FollowPath(Vector3[] wayPoints) {
        transform.position = wayPoints[0];
        int targetWayPointIndex = 1;
        int numberOfWayPoints = wayPoints.Length;
        transform.LookAt(wayPoints[targetWayPointIndex]);

        while (true) {
            yield return StartCoroutine(MoveTo(wayPoints[targetWayPointIndex]));
            targetWayPointIndex = (targetWayPointIndex + 1) % numberOfWayPoints;
            yield return new WaitForSeconds(waitingTime);
            yield return StartCoroutine(RotateGuardTo(wayPoints[targetWayPointIndex]));
        }
    }

    //Guard rotation coroutine
    IEnumerator RotateGuardTo(Vector3 targetWayPoint) {
        Vector3 targetDirection = (targetWayPoint - transform.position).normalized;
        float targetAngleInDegrees = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y,targetAngleInDegrees))>0.05f) {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngleInDegrees, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    //Coroutine to move from one point to another
    IEnumerator MoveTo(Vector3 destination) {
        while (transform.position!=destination) {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // To draw gizmos in the editor
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
