using UnityEngine;
using System.Collections;

public class FlipperMotor : MonoBehaviour
{

	private Rigidbody rb;
	public float ForceAmount;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate ()
	{
		float left = Input.GetAxis ("Horizontal");
		float right = Input.GetAxis ("Vertical");


		if (left < 0) {
			if (rb.CompareTag ("Left")) {
				rb.AddForce (0, 0, ForceAmount);
			}
		}
		if (right > 0) {
			if (rb.CompareTag ("Right")) {
				rb.AddForce (0, 0, ForceAmount);
			}
		}

		//rb.AddTorque (0, move * TorqueAmount, 0);
	}
}
