using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;
    public GameObject crosshair; // Crosshair UI referansı

    private bool isFirstPerson = false;

    void Start()
    {
        // Kameraları ve crosshair'i doğru şekilde başlat
        SwitchCamera(isFirstPerson);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            SwitchCamera(isFirstPerson);
        }
    }

    void SwitchCamera(bool firstPerson)
    {
        firstPersonCamera.enabled = firstPerson;
        thirdPersonCamera.enabled = !firstPerson;

        // Crosshair doğru moda göre aktif/pasif yapılır
        if (crosshair != null)
        {
            crosshair.SetActive(firstPerson); // sadece FPS modda açık
        }
    }
}
