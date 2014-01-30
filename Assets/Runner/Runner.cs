using UnityEngine;

public class Runner : MonoBehaviour {
	
	public static int Boosts, DistanceTraveled;

	public float Acceleration, GameOverY;
	public Vector3 JumpVelocity, BoostVelocity;
	
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

	void FixedUpdate () {
		if(touchingPlatform){
			rigidbody.AddForce(Acceleration, 0f, 0f, ForceMode.Acceleration);

			if (Input.GetButtonDown("Jump")) {
				rigidbody.AddForce(JumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
		}
		else if (Input.GetButtonDown("Jump") && Boosts > 0) {	// extra code not worth the DRY
			rigidbody.AddForce(BoostVelocity, ForceMode.VelocityChange);
			Boosts -= 1;
			GUIManager.SetBoosts(Boosts);
		}

		DistanceTraveled = (int)transform.localPosition.x;
		GUIManager.SetScore(DistanceTraveled);
		
		if (transform.localPosition.y < GameOverY) { // if falls
			GameEventManager.TriggerGameOver();
		}
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}

	private void GameStart () {
		GUIManager.SetScore(DistanceTraveled = 0);
		GUIManager.SetBoosts(Boosts = 0);
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