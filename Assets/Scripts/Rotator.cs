using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
		//transform.Translate (new Vector3 (0.0f, (float)((Mathf.Sin (Time.deltaTime)) * 0.5), 0.0f));
	}
}
