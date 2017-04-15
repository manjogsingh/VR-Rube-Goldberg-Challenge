using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
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

	//Dash
	public float dashSpeed=0.1f;
	private bool isDashing;
	private float lerpTime;
	private Vector3 dashStartPosition;

	//Walking
	private Vector3 movementDirection;
	public Transform playerCam;
	public float moveSpeed=4f;


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

		if(device.GetPress(SteamVR_Controller.ButtonMask.Grip))
		{
			movementDirection=playerCam.transform.position;
			movementDirection=new Vector3(movementDirection.x,0,movementDirection.z);
			movementDirection*=moveSpeed*Time.deltaTime;
			player.transform.position+=movementDirection;
		}

        if (isDashing)
        {
            lerpTime = Time.deltaTime * dashSpeed;
            player.transform.position = Vector3.Lerp(dashStartPosition, teleportLocation, lerpTime);
            if (lerpTime >= 1)
            {
                isDashing = false;
                lerpTime = 0;
            }
        }
        else
        {
            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                laser.gameObject.SetActive(true);
                teleportAimer.SetActive(true);

                laser.SetPosition(0, gameObject.transform.position);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 15, laserMask))
                {
                    teleportLocation = hit.point;
                    laser.SetPosition(1, teleportLocation);
                    teleportAimer.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudge, teleportLocation.z);
                }
                else
                {
                    teleportLocation = new Vector3(transform.forward.x * 15 + transform.position.x, transform.forward.y * 15 + transform.position.y, transform.forward.z * 15 + transform.position.z);
                    RaycastHit groundRay;
                    if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask))
                    {
                        teleportLocation = new Vector3(transform.forward.x * 15 + transform.position.x, groundRay.point.y, transform.forward.z * 15 + transform.position.z);
                    }
                    laser.SetPosition(1, transform.forward * 15 + transform.position);
                    teleportAimer.transform.position = teleportLocation + new Vector3(0, yNudge, 0);
                }
            }
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            laser.gameObject.SetActive(false);
            teleportAimer.SetActive(false);
            //player.transform.position=teleportLocation;
            dashStartPosition = player.transform.position;
            isDashing = true;
        }
    }
}
