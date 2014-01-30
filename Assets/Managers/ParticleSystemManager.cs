using UnityEngine;
using System.Collections;

public class ParticleSystemManager : MonoBehaviour {

	public ParticleSystem[] ParticleSystems;

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameOver();
	}

	private void GameStart () {
		for (int i = 0; i < ParticleSystems.Length; i++) {
			ParticleSystems[i].Clear();
			ParticleSystems[i].enableEmission = true;
		}
	}

	private void GameOver () {
		for (int i = 0; i < ParticleSystems.Length; i++) {
			ParticleSystems[i].enableEmission = false;
		}
	}
}
