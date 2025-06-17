using UnityEngine;
using TMPro;

public class FPSInteractor : MonoBehaviour
{
    [Header("Kamera ve Ray Ayarları")]
    public Camera fpsCamera;
    public float interactRange = 2f;
    public LayerMask interactableLayer;

    [Header("UI")]
    public TMP_Text targetNameText;

    [Header("Nesne Taşıma Ayarları")]
    public Transform holdPoint;
    private GameObject heldObject = null;
    private Rigidbody heldRb = null;
    private Collider heldCollider = null;

    private void Update()
    {
        if (fpsCamera == null) return;

        if (heldObject == null)
        {
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
            {
                // UI güncelle
                if (targetNameText != null)
                {
                    targetNameText.gameObject.SetActive(true);
                    targetNameText.text = hit.collider.gameObject.name;
                }

                // Nesne alma
                if (Input.GetKeyDown(KeyCode.F) && hit.collider.CompareTag("interactable"))
                {
                    PickupObject(hit.collider.gameObject);
                }

                // NPC ile etkileşim
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.TryGetComponent(out NPCInteractable npc))
                    {
                        npc.Interact();
                    }
                }
            }
            else
            {
                if (targetNameText != null)
                    targetNameText.gameObject.SetActive(false);
            }
        }
        else
        {
            // Taşıma sırasında nesnenin pozisyonu sabitlenir ve dönmesi engellenir
            heldObject.transform.position = holdPoint.position;
            heldObject.transform.rotation = Quaternion.identity;

            if (Input.GetKeyDown(KeyCode.F))
            {
                DropObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogueUI dialogueUI = FindObjectOfType<DialogueUI>();
            dialogueUI?.HideDialogue();
        }
    }

    private void PickupObject(GameObject obj)
    {
        heldObject = obj;
        heldRb = obj.GetComponent<Rigidbody>();
        heldCollider = obj.GetComponent<Collider>();

        if (heldRb != null)
        {
            heldRb.linearVelocity = Vector3.zero;
            heldRb.angularVelocity = Vector3.zero;
            heldRb.useGravity = false;
            heldRb.isKinematic = true;
        }

        if (heldCollider != null)
        {
            heldCollider.enabled = false;
        }

        obj.transform.SetParent(holdPoint);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }

    private void DropObject()
    {
        if (heldObject == null) return;

        heldObject.transform.SetParent(null);

        if (heldRb != null)
        {
            heldRb.isKinematic = false;
            heldRb.useGravity = true;
        }

        if (heldCollider != null)
        {
            heldCollider.enabled = true;
        }

        heldObject = null;
        heldRb = null;
        heldCollider = null;
    }
}
