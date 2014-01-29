using UnityEngine;

public class Runner : MonoBehaviour {

	public static float distanceTraveled;
	
	public float acceleration;
	public Vector3 jumpVelocity;
	public float gameOverY;

	public static bool PoweredUp;

	private bool touchingPlatform;
	private Vector3 startPosition;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	void Update () {
		if (Input.GetButtonDown("Jump") && touchingPlatform) {
			rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
			touchingPlatform = false;
		}
		else if (Input.GetButtonDown("Jump") && PoweredUp) {
			rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
			PoweredUp = false;
		}
		distanceTraveled = transform.localPosition.x;
		
		if (transform.localPosition.y < gameOverY) {
			GameEventManager.TriggerGameOver();
		}
	}

	void FixedUpdate () {
		if(touchingPlatform){
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}

	private void GameStart () {
		distanceTraveled = 0f;
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
}