using UnityEngine;
using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

// Change the material when certain poses are made with the Myo armband.
// Vibrate the Myo armband when a fist pose is made.
public class MyoControl : MonoBehaviour
{
	// Myo game object to connect with.
	// This object must have a ThalmicMyo script attached.
	public GameObject myo = null;
	public int force;
	public GameObject theCamera;
	public float timeMultiplier;
	public float timeSlowed;
	// The pose from the last update. This is used to determine if the pose has changed
	// so that actions are only performed upon making them rather than every frame during
	// which they are active.
	private Pose _lastPose = Pose.Unknown;
	private Rigidbody rb;
	private bool slowed;
	private float timeStart;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		slowed = false;
	}
	
	// Update is called once per frame.
	void Update ()
	{
		actions ();
	}
	
	// Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
	// recognized.
	void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
	{
		ThalmicHub hub = ThalmicHub.instance;
		
		if (hub.lockingPolicy == LockingPolicy.Standard) {
			myo.Unlock (UnlockType.Timed);
		}
		
		myo.NotifyUserAction ();
	}
	
	private void actions(){
		// Access the ThalmicMyo component attached to the Myo game object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		
		//Camera Direction setup
		//Transform cameraTransform = theCamera.transform;
		// forward of the camera on the x-z plane
		Vector3 cameraForward = theCamera.transform.TransformDirection(Vector3.forward);
		cameraForward.y = 0f;
		cameraForward = cameraForward.normalized;
		
		Vector3 cameraRight = new Vector3(cameraForward.z, 0.0f, -cameraForward.x);
		Vector3 cameraLeft = new Vector3(-cameraForward.z, 0.0f, cameraForward.x);

		if (slowed && Time.unscaledTime - timeStart > timeSlowed) {
			Time.timeScale = 1.0f;
			rb.velocity = timeMultiplier * rb.velocity;
			slowed = false;
			Debug.Log ("Time");
		}

		// Check if the pose has changed since last update.
		// The ThalmicMyo component of a Myo game object has a pose property that is set to the
		// currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
		// detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
		// is not on a user's arm, pose will be set to Pose.Unknown.
		if (thalmicMyo.pose != _lastPose) {
			_lastPose = thalmicMyo.pose;
			

			
			// Vibrate the Myo armband when a fist is made.
			if (thalmicMyo.pose == Pose.Fist) {
				thalmicMyo.Vibrate (VibrationType.Medium);
				
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
				
				// Change material when wave in, wave out or double tap poses are made.
			} else if (thalmicMyo.pose == Pose.WaveIn) {
				rb.AddForce(cameraLeft * force);
				
			} else if (thalmicMyo.pose == Pose.WaveOut) {
				rb.AddForce(cameraRight * force);
				
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			} else if (thalmicMyo.pose == Pose.DoubleTap) {
				if(Time.timeScale == timeMultiplier){
					Time.timeScale = 1.0f;
					rb.velocity = timeMultiplier * rb.velocity;
					slowed = false;
				}else{
					Time.timeScale = timeMultiplier;
					slowed = true;
					timeStart = Time.unscaledTime;
				}
				//rb.AddForce (new Vector3(0.0f, force * 10, 0.0f));
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			}
		}
		
	}
}

