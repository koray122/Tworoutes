using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;

    public float throwForce = 500f;
    public float pickUpRange = 5f;
    private float rotationSensitivity = 1f;

    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool canDrop = true;
    private int LayerNumber;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping();
                    DropObject();
                }
            }
        }

        if (heldObj != null)
        {
            MoveObject();
            RotateObject();

            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true)
            {
                StopClipping();
                ThrowObject();
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObj.transform.parent = holdPos.transform;
            heldObj.layer = LayerNumber;
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;

        // Nesneyi dik konuma getir
        heldObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        heldObj = null;
    }

    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;

        // Nesneyi her frame dik tut
        heldObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))
        {
            canDrop = false;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;

            // Eğer elindeki nesneyi döndürmek istersen bu kısmı açabilirsin
            heldObj.transform.Rotate(Vector3.down, XaxisRotation, Space.World);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation, Space.World);
        }
        else
        {
            canDrop = true;
        }
    }

    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;

        // Nesneyi dik konuma getir
        heldObj.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        heldObjRb.AddForce(transform.forward * throwForce);

        heldObj = null;
    }

    void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);

        if (hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}
