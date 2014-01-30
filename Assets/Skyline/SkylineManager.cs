using UnityEngine;
using System.Collections.Generic;

public class SkylineManager : MonoBehaviour {
	// Similar to PlatformManager
	public Transform Prefab;
	public int NumberOfObjects;
	public float RecycleOffset;
	public Vector3 StartPosition, MinSize, MaxSize;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(NumberOfObjects);
		for(int i = 0; i < NumberOfObjects; i++){
			objectQueue.Enqueue((Transform)Instantiate(
				Prefab, new Vector3(0f, 0f, -100f), Quaternion.identity));
		}

		enabled = false;
	}

	void Update () {
		if (objectQueue.Peek().localPosition.x + RecycleOffset < Runner.DistanceTraveled) {
			Recycle();
		}
	}

	private void Recycle () {
		Vector3 scale = new Vector3(
			Random.Range(MinSize.x, MaxSize.x),
			Random.Range(MinSize.y, MaxSize.y),
			Random.Range(MinSize.z, MaxSize.z));

		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue(o);
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