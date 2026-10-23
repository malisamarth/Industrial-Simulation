using UnityEngine;

public class AimStateManager : MonoBehaviour {

    [SerializeField] private Transform camFollowPosition;

    public float mouseSensitivity = 300f;
    public float smoothSpeed = 10f;

    float xRotation;
    float yRotation;

    float currentX;
    float currentY;

    private void Update() {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -80, 80);

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -40f, 70f);
    }

    private void LateUpdate() {


        currentX = Mathf.Lerp(currentX, xRotation, smoothSpeed * Time.deltaTime);
        currentY = Mathf.Lerp(currentY, yRotation, smoothSpeed * Time.deltaTime);

        camFollowPosition.localRotation = Quaternion.Euler(currentX, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, currentY, 0f);
    }
}