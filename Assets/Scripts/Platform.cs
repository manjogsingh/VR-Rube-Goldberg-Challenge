using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public bool onPlatform;

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
		Debug.Log(other.gameObject.name);
        if(other.gameObject.CompareTag("MainCamera"))//if (other.gameObject.name == "Camera (head)")
        {
            onPlatform = true;
			Debug.Log("on platform");
        }
        else
        {
            onPlatform = false;
			Debug.Log("not platform");
        }
    }
}
