using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float launchForce;

	public Text livesText;

	public int lives;

	private int livesCopy;

	public Text scoreText;

	private int score;

	private Rigidbody rb;

	private bool dead;

	private bool launch;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		score = 0;
		scoreText.text = "Score: " + score.ToString();
		livesText.text = "Lives: " + lives;
		livesCopy = lives;
		dead = false;
	}

	void FixedUpdate () {
		if (dead) {
			if (livesCopy < 1) {
				if (Input.GetKey ("r")) {
					score = 0;
					livesCopy = lives;
					livesText.text = "Lives: " + livesCopy;
					rb.position = new Vector3 ((float)9.008, (float)0.5, (float)-18.76);
					dead = false;
				}
			} else {
				rb.position = new Vector3 ((float)9.008, (float)0.5, (float)-18.76);
				dead = false;
			}		
		} else if (launch) {
			if (Input.GetKey ("up")) {
				rb.AddForce (0, 0, launchForce);
			}
		}
	}
	
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag ("Points - 10")) {
			score += 10;
			scoreText.text = "Score: " + score.ToString ();
		} else if (col.gameObject.CompareTag ("Points - 25")) {
			score += 25;
			scoreText.text = "Score: " + score.ToString ();
		} else if (col.gameObject.CompareTag ("Points - 50")) {
			score += 50;
			scoreText.text = "Score: " + score.ToString ();
		} else if (col.gameObject.CompareTag ("Points - 100")) {
			score += 100;
			scoreText.text = "Score: " + score.ToString ();
		} else if (col.gameObject.CompareTag ("Points - 1000")) {
			score += 1000;
			scoreText.text = "Score: " + score.ToString ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Points - 10")) {
			score += 10;
			scoreText.text = "Score: " + score.ToString ();
		} else if (other.CompareTag ("Points - 25")) {
			score += 25;
			scoreText.text = "Score: " + score.ToString ();
		} else if (other.CompareTag ("Points - 50")) {
			score += 50;
			scoreText.text = "Score: " + score.ToString ();
		} else if (other.CompareTag("Points - 100")) {
			score += 100;
			scoreText.text = "Score: " + score.ToString ();
		} else if (other.CompareTag ("Points - 1000")) {
			score += 1000;
			scoreText.text = "Score: " + score.ToString ();
		} else if (other.CompareTag("Fall out")) {
			//Run fall out function
			livesCopy--;
			livesText.text = "Lives: " + livesCopy;
			dead = true;
		}

	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag("Respawn")) {
			launch = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Respawn")) {
			launch = false;
		}
	}

}