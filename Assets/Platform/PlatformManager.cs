using UnityEngine;
using System.Collections.Generic;

// Datatype for combining two material types in a Materials array
[System.Serializable]  
public class PlatformMats {
	public Material CMat;
	public PhysicMaterial PMat;
}

public class PlatformManager : MonoBehaviour {
	public Transform Prefab;
	public int NumberOfObjects;
	public float RecycleOffset;
	public Vector3 StartPosition;
	public Vector3 MinSize, MaxSize, MinGap, MaxGap;	// Platform sizes
	public float MinY, MaxY;	// Height limits
	public PlatformMats[] Materials;
	public Booster Booster;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(NumberOfObjects);	// Instantiate platforms
		for (int i = 0; i < NumberOfObjects; i++) {
			objectQueue.Enqueue((Transform)Instantiate(
				Prefab, new Vector3(0f, 0f, -100f), Quaternion.identity));
		}
		enabled = false;
	}

	void Update () {
		if(objectQueue.Peek().localPosition.x + RecycleOffset < Runner.DistanceTraveled){
			Recycle();
		}
	}

	private void Recycle () {	// Position, resize, add materials to platforms
		Vector3 scale = new Vector3(
			Random.Range(MinSize.x, MaxSize.x),
			Random.Range(MinSize.y, MaxSize.y),
			Random.Range(MinSize.z, MaxSize.z));

		Vector3 position = nextPosition + new Vector3 (scale.x * 0.5f, scale.y * 0.5f, 0f);
		Booster.SpawnIfAvailable(position);		

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		int materialIndex = Random.Range(0, Materials.Length);
		o.renderer.material = Materials[materialIndex].CMat;
		o.collider.material = Materials[materialIndex].PMat;
		objectQueue.Enqueue(o);

		nextPosition += new Vector3(	// Update nextPosition
			Random.Range(MinGap.x, MaxGap.x) + scale.x,
			Random.Range(MinGap.y, MaxGap.y),
			Random.Range(MinGap.z, MaxGap.z));

		if(nextPosition.y < MinY){	// if it's too low
			nextPosition.y = MinY + MaxGap.y;
		}
		else if(nextPosition.y > MaxY){
			nextPosition.y = MaxY - MaxGap.y;
		}
	}
	
	private void GameStart () {
		nextPosition = StartPosition;
		for(int i = 0; i < NumberOfObjects; i++){
			Recycle();
		}
		enabled = true;
	}

	private void GameOver () {
		enabled = false;
	}
}