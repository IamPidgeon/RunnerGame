using UnityEngine;
using System.Collections;


// assign it inside SkylineManager, but not very OOP
// should disappear if GameOver
// I need to know the position of the skyline platforms?


public class BoosterManager : MonoBehaviour {

	public Vector3 offset, rotationVelocity;		// offset from platform's center, velocity for spin
	public float recycleOffset, spawnChance;		// offset from Runner/spawn position, % chance for spawn
	public GameObject prefab;

	/* void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive (false);
	}
	*/

	public void SpawnIfAvailable (Vector3 position) {	// spawns if booster is not already active and > % spawnChance
		if (spawnChance >= Random.Range(0, 100)) {
			Instantiate(prefab, new Vector3(position.x + recycleOffset, position.y + offset.y, position.z), Quaternion.identity);
		}
	}

	private void OnTriggerEnter() {
		Destroy(gameObject);
		Runner.PoweredUp = true;
	}

}
