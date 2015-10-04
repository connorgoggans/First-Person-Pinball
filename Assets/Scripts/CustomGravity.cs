using UnityEngine;
using System.Collections;

public class CustomGravity : MonoBehaviour {

	private Rigidbody rb;
	
	public float gravity;
	
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () 
	{
		rb.AddForce (0, 0, -gravity);
	}
}
