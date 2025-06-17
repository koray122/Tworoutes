using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform cameraPivot;      // X ekseni (yukarı-aşağı)
    public Transform characterBody;    // Y ekseni (sağa-sola)

    private float xRotation = 0f;
    private float yRotation = 0f;

    [Header("Dönüş Limitleri")]
    public float minVerticalAngle = -90f;   // Yukarı-aşağı minimum açı
    public float maxVerticalAngle = 90f;    // Yukarı-aşağı maksimum açı

    public bool canLook = true;  // Kamera kontrol aktif mi?

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Başlangıç dönüş açılarını al
        Vector3 initialEuler = characterBody.localEulerAngles;
        yRotation = initialEuler.y;
    }

    void Update()
    {
        if (!GetComponent<Camera>().enabled) return;
        if (!canLook) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yukarı-aşağı (dikey) dönüş
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);
        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Sağa-sola (yatay) dönüş
        yRotation += mouseX;
        characterBody.rotation = Quaternion.Euler(0f, yRotation, 0f); // Dünya dönüşü (global)
    }
}
