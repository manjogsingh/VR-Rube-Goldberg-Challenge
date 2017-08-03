using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCheat : MonoBehaviour
{
    public bool onPlatform;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger"+other.gameObject.name);
        if (other.gameObject.CompareTag("Platform"))
        {
            onPlatform = true;
            Debug.Log("on platform");
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            onPlatform = false;
        }
    }
}
