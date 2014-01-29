using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	// offset from platform's center, velocity for spin
	public Vector3 offset, rotationVelocity; 
	// offset for when object moves outside camera view, % chance for spawn
	public float recycleOffset, spawnChance;

	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}

	void Update () {
		// If booster is behind runner and off camera
		if (transform.localPosition.x + recycleOffset < Runner.distanceTraveled) {
			gameObject.SetActive(false);
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	public void SpawnIfAvailable (Vector3 position) {	
		// spawns if booster is not already active and > % spawnChance
		if (gameObject.activeSelf || spawnChance <= Random.Range (0f, 100f)) {
			return;
		}
		transform.localPosition = position + offset;
		gameObject.SetActive(true);
	}

	private void GameOver () {
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter() {
		Runner.PowerUps += 1;
		GUIManager.SetPowerUps(Runner.PowerUps);
		gameObject.SetActive(false);
	}

}
