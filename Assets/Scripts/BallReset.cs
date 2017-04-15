using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

	private Vector3 startPosition;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		startPosition=transform.position;
		rb=GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Ground"))
		{
			transform.position=startPosition;
			rb.velocity=new Vector3(0,0,0);
			rb.angularVelocity=new Vector3(0,0,0);
		}
	}
}
