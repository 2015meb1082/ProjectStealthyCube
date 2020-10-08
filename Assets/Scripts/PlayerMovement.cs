using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 40f;
    [SerializeField]
    private float turnSpeed = 90f;


    [SerializeField]
    private float smoothTime = 0.1f;
    private float smoothedInputMagnitude;
    private float smoothMoveVelocity;

    private float smoothedAngle;

    private Rigidbody rb;
    private Vector3 currentVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
        float inputMagnitude = inputDir.magnitude;
        if (inputMagnitude != 0) {
            smoothedInputMagnitude = Mathf.SmoothDamp(smoothedInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothTime);
            // transform.Translate(transform.forward * moveSpeed * smoothedInputMagnitude * Time.deltaTime, Space.World);

            float angle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;
            smoothedAngle = Mathf.LerpAngle(smoothedAngle, angle, turnSpeed * Time.deltaTime);
            // transform.eulerAngles = Vector3.up * smoothedAngle;
            currentVelocity = transform.forward * moveSpeed * inputMagnitude;
        }
        else {
            currentVelocity = Vector3.zero;
        }
  
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentVelocity * Time.deltaTime);
        rb.MoveRotation(Quaternion.Euler(0,smoothedAngle,0));
    }
}
