using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;

    //Telepoter
    private LineRenderer laser;
    public GameObject teleportAimer;
    public Vector3 teleportLocation;
    public GameObject player;
    public LayerMask laserMask;
    public float yNudge = 1f;
    public bool isLeftHand;
    public float throwForce = 1.5f;

    //Swipe
    public float swipeSum;
    public float touchLast;
    public float touchCurrent;
    public float distance;
    public bool hasSwipedLeft;
    public bool hasSwipedRight;
    public GameObject objectMenu;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        laser = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (isLeftHand)
        {
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.gameObject.SetActive(true);
                teleportAimer.SetActive(true);

                laser.SetPosition(0, gameObject.transform.position);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 10, laserMask))
                {
                    teleportLocation = hit.point;
                    laser.SetPosition(1, teleportLocation);
                    teleportAimer.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudge, teleportLocation.z);
                }
                else
                {
                    teleportLocation = new Vector3(transform.forward.x * 10 + transform.position.x, transform.forward.y * 10 + transform.position.y, transform.forward.z * 10 + transform.position.z);
                    RaycastHit groundRay;
                    if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask))
                    {
                        teleportLocation = new Vector3(transform.forward.x * 10 + transform.position.x, groundRay.point.y, transform.position.z + transform.forward.z * 10);
                    }
                    laser.SetPosition(1, transform.forward * 10 + transform.position);
                    teleportAimer.transform.position = teleportLocation + new Vector3(0, yNudge, 0);
                }
            }

            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.gameObject.SetActive(false);
                teleportAimer.SetActive(false);
                player.transform.position = teleportLocation;
            }
        }
        else
        {
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                objectMenu.SetActive(true);
                touchLast = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
            }
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                SpawnObject();
                Debug.Log("lol");
            }
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
            {
                touchCurrent = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
                distance = touchCurrent - touchLast;
                touchLast = touchCurrent;
                swipeSum += distance;
                if (!hasSwipedRight)
                {
                    if (swipeSum > 0.5f)
                    {
                        swipeSum = 0;
                        SwipeRight();
                        hasSwipedRight = true;
                        hasSwipedLeft = false;
                    }
                }
                if (!hasSwipedLeft)
                {
                    if (swipeSum < -0.5f)
                    {
                        swipeSum = 0;
                        SwipeLeft();
                        hasSwipedLeft = true;
                        hasSwipedRight = false;
                    }
                }
            }
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                swipeSum = 0;
                touchCurrent = 0;
                touchLast = 0;
                hasSwipedLeft = false;
                hasSwipedRight = false;
                objectMenu.SetActive(false);
            }
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(other);
            }
            else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ThrowObject(other);
            }
        }
        else if (other.gameObject.CompareTag("Structure"))
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(other);
            }
            else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                DropObject(other);
            }
        }
    }
    void ThrowObject(Collider col)
    {
        col.transform.SetParent(null);
        Rigidbody rb = col.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = device.velocity * throwForce;
        rb.angularVelocity = device.angularVelocity;
        Debug.Log("Realsed the object");
    }
    void GrabObject(Collider col)
    {
        col.transform.SetParent(gameObject.transform);
        col.GetComponent<Rigidbody>().isKinematic = true;
        device.TriggerHapticPulse(2000);
        Debug.Log("You are grabing a object");
    }
    void DropObject(Collider col)
    {
        col.transform.SetParent(null);
        col.GetComponent<Rigidbody>().isKinematic = false;
    }

    void SwipeLeft()
    {
        objectMenu.GetComponent<ObjectManager>().MenuLeft();
        Debug.Log("swipe left");
    }
    void SwipeRight()
    {
        objectMenu.GetComponent<ObjectManager>().MenuRight();
        Debug.Log("swipe right");
    }
    void SpawnObject()
    {
        objectMenu.GetComponent<ObjectManager>().SpawnCurrentObject();
    }
}
