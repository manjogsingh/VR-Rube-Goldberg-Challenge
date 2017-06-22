using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{

    private Vector3 startPosition;
    private Rigidbody rb;
    GameObject[] collectables;
    private int collectableCount = 0;
    private bool ballState;
    public bool areAllCollected;

    public GameObject platformArea;
    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        collectables = GameObject.FindGameObjectsWithTag("Collectable");
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

        if (platformArea.GetComponent<Platform>().onPlatform)
        {
            ballState = true;
        }
        else
        {
            ballState = false;
        }

        if (ballState)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
            transform.position = startPosition;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);

            foreach (GameObject obj in collectables)
            {
                obj.SetActive(true);
                collectableCount = 0;
            }
        }
        if (other.gameObject.CompareTag("Collectable"))
        {
            //collectable code
            other.gameObject.SetActive(false);
            collectableCount++;
        }
    }
}
