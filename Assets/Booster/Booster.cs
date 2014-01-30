using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	// offset from platform's center, velocity for spin
	public Vector3 Offset, RotationVelocity; 
	// offset for when object moves outside camera view, % chance for spawn
	public float RecycleOffset, SpawnChance;

	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}

	void Update () {
		// If booster is behind runner and off camera
		if (transform.localPosition.x + RecycleOffset < Runner.DistanceTraveled) {
			gameObject.SetActive(false);
			return;
		}
		transform.Rotate(RotationVelocity * Time.deltaTime);
	}

	public void SpawnIfAvailable (Vector3 position) {	
		// don't spawn if booster is active or SpawnChance fails
		if (gameObject.activeSelf || SpawnChance <= Random.Range (0f, 100f)) {
			return;
		}
		transform.localPosition = position + Offset;
		gameObject.SetActive(true);
	}

	private void GameOver () {
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter() {
		Runner.Boosts += 1;
		GUIManager.SetBoosts(Runner.Boosts);
		gameObject.SetActive(false);
	}

}
