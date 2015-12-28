using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 0.1f;
    public float rotationSpeed = 4f;
    public float smoothness = 0.85f;

    private Vector3 targetPosition;

    public Quaternion targetRotation;
    private float targetRotationY;
    private float targetRotationX;

    // Use this for initialization
    private void Start()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;
        targetRotationY = transform.localRotation.eulerAngles.y;
        targetRotationX = transform.localRotation.eulerAngles.x;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            targetPosition += transform.forward * movementSpeed;
        if (Input.GetKey(KeyCode.A))
            targetPosition -= transform.right * movementSpeed;
        if (Input.GetKey(KeyCode.S))
            targetPosition -= transform.forward * movementSpeed;
        if (Input.GetKey(KeyCode.D))
            targetPosition += transform.right * movementSpeed;
        if (Input.GetKey(KeyCode.Q))
            targetPosition -= transform.up * movementSpeed;
        if (Input.GetKey(KeyCode.E))
            targetPosition += transform.up * movementSpeed;

        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            targetRotationY += Input.GetAxis("Mouse X") * rotationSpeed;
            targetRotationX -= Input.GetAxis("Mouse Y") * rotationSpeed;
            targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);
        }
        else
            Cursor.visible = true;

        transform.position = Vector3.Lerp(transform.position, targetPosition, (1.0f - smoothness));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (1.0f - smoothness));
    }
}