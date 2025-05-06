using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;

    private bool isFirstPerson = false;

    void Start()
    {
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
    }
}
