using UnityEngine;

public class Runner : MonoBehaviour {

	public static float distanceTraveled;
	
	public float acceleration;
	public Vector3 jumpVelocity;
	public Vector3 boostVelocity;
	public float gameOverY;

	public static int PowerUps;

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
		if (Input.GetButtonDown("Jump")) {
			if (touchingPlatform) {
				rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
			else if (PowerUps > 0) {
				rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
				PowerUps -= 1;
				GUIManager.SetPowerUps(PowerUps);
			}
		}
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetDistance(distanceTraveled);
		GUIManager.SetPowerUps(PowerUps);

		
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
		PowerUps = 0;
		enabled = true;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
}