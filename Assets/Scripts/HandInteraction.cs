using UnityEngine;
public class HandInteraction : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public float throwForce = 1.5f;

    //Swipe
    public float swipeSum;
    public float touchLast;
    public float touchCurrent;
    public float distance;
    public bool hasSwipedLeft;
    public bool hasSwipedRight;
    public ObjectManager objectManager;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            touchLast=device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
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
        if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            swipeSum=0;
            touchCurrent=0;
            touchLast=0;
            hasSwipedLeft=false;
            hasSwipedRight=false;
        }
        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            SpawnObject();
        }
    }

    void SwipeLeft()
    {
        objectManager.MenuLeft();
        Debug.Log("swipe left");
    }
    void SwipeRight()
    {
        objectManager.MenuRight();
        Debug.Log("swipe right");
    }
    void SpawnObject()
    {
        objectManager.SpawnCurrentObject();
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider col
    /// that is touching the trigger.
    /// </summary>
    /// <param name="col">The col Collider involved in this collision.</param>
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ThrowObject(col);
            }
            else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(col);
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
}