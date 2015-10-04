using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {

	public GameObject bumper;

	public float explosionForce;
	public float explosionRadius;

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Ball")) {
			col.rigidbody.AddExplosionForce(explosionForce, bumper.transform.position, explosionRadius);
		}
	}

}
