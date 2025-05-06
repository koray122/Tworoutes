using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform cameraPivot;      // X ekseni (yukarı-aşağı)
    public Transform characterBody;    // Y ekseni (sağa-sola)

    private float xRotation = 0f;
    private float yRotation = 0f;

    // Y ekseni (sağa-sola) sınırları
    public float yRotationLimit = 135f; // total: 270 derece

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!GetComponent<Camera>().enabled) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // X ekseni: yukarı-aşağı bakış
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Y ekseni: sağa-sola bakış (sınırlı)
        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -yRotationLimit, yRotationLimit);
        characterBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
