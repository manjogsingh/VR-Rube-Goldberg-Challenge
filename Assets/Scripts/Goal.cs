using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject cameraEye;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Throwable"))
        {
            //check for collectables otherwise reset
            //new scene
            if (other.gameObject.GetComponent<BallReset>().areAllCollected&&cameraEye.GetComponent<AntiCheat>().onPlatform)
            {
                Debug.Log("Yes Goal");
            }
            else
            {
                Debug.Log("No Goal");
            }
        }
    }
}
