using UnityEngine;

public class PickUp : MonoBehaviour
{
    bool isHolding = false;

    [SerializeField] float throwForce = 0f;
    [SerializeField] float maxDistance = 3f;
    [SerializeField] Camera mainCamera;

    Rigidbody rb;
    Vector3 holdOffset = new Vector3(0, 0, 1.7f); // Kameranın önünde tutulacak mesafe

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (mainCamera == null)
        {
            Debug.LogError("Ana kamera atanmadı!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isHolding)
            {
                Throw();
            }
            else
            {
                TryPickUp();
            }
        }

        if (isHolding)
        {
            Hold();
        }
    }

    private void TryPickUp()
    {
        if (mainCamera == null) return;

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isHolding = true;
                rb.useGravity = false;

                // Sadece dönmeyi kısıtla, pozisyon serbest
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    private void Hold()
    {
        Vector3 targetPos = mainCamera.transform.position + mainCamera.transform.forward * holdOffset.z;

        // Nesneyi kameranın önünde MovePosition ile taşı
        rb.MovePosition(targetPos);

        // Nesnenin kendi kendine hareket etmesini ve dönmesini engelle
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void Throw()
    {
        if (isHolding)
        {
            isHolding = false;
            rb.useGravity = true;

            // Kısıtlamaları kaldır, fizik özgür kalsın
            rb.constraints = RigidbodyConstraints.None;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.AddForce(mainCamera.transform.forward * throwForce, ForceMode.Impulse);
        }
    }

    private void Drop()
    {
        if (isHolding)
        {
            isHolding = false;
            rb.useGravity = true;

            // Kısıtlamaları kaldır
            rb.constraints = RigidbodyConstraints.None;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
