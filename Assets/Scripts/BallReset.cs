using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{

    private Vector3 startPosition;
    private Rigidbody rb;
    GameObject[] collectables;
    private int collectableCount = 0;
    public bool areAllCollected;
    public GameObject platform;
    public GameObject IntroCanvas;
    private bool hasMoved;
    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        hasMoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collectables.Length == collectableCount)
        {
            areAllCollected = true;
        }
        else
        {
            areAllCollected = false;
        }

        if (platform.GetComponent<AntiCheat>().onPlatform)
        {
            gameObject.layer = 0;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
        else
        {
            gameObject.layer = 8;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ResetBall();
            foreach (GameObject obj in collectables)
            {
                obj.SetActive(true);
                collectableCount = 0;
            }
        }

        if (other.gameObject.CompareTag("Teleport"))
        {

            GameObject[] obj = GameObject.FindGameObjectsWithTag("Teleport");

            if (obj.Length == 2)
            {
                foreach (GameObject ob in obj)
                {
                    if (ob != other.gameObject && !hasMoved)
                    {
                        RaycastHit rh = new RaycastHit();
                        Debug.DrawRay(ob.transform.position, ob.transform.forward, Color.red, 10000);
                        if (Physics.Linecast(ob.transform.position, ob.transform.forward, out rh))
                        {
                            hasMoved = true;
                            transform.position = ob.transform.position + new Vector3(0, 0, 0.5f);
                            rb.velocity = ob.transform.forward * rb.velocity.magnitude * 2;
                        }
                    }
                    else
                    {
                        hasMoved = false;
                    }
                }
            }
            else
            {
                ResetBall();
            }
        }
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            //collectable code
            other.gameObject.SetActive(false);
            collectableCount++;
        }
        if(other.gameObject.name.Equals("Next"))
        {
            IntroCanvas.GetComponent<IntroUI>().NextClick();
        }
        else if(other.gameObject.name.Equals("Back"))
        {
            IntroCanvas.GetComponent<IntroUI>().BackClick();
        }
        else if(other.gameObject.name.Equals("Done"))
        {
            IntroCanvas.GetComponent<IntroUI>().DoneClick();
        }
    }

    void ResetBall()
    {
        transform.position = startPosition;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
    }
}
