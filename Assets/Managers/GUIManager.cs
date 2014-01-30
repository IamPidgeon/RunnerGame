using UnityEngine;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {

	public GUIText GameOverText, InstructionsText, RunnerText, ScoreText, BoostsText;

	// creating an instance which is necessary to call non-static methods
	private static GUIManager instance;		
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameOverText.enabled = false;
		ScoreText.enabled = false;
		BoostsText.enabled = false;
	}

	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		GameOverText.enabled = false;
		InstructionsText.enabled = false;
		RunnerText.enabled = false;
		ScoreText.enabled = true;
		BoostsText.enabled = true;
		enabled = false;
	}
	
	private void GameOver () {
		GameOverText.enabled = true;
		InstructionsText.enabled = true;
		enabled = true;
	}

	// setters for Runner to send info to update the GUI
	public static void SetBoosts(int boosts){		
		instance.BoostsText.text = "Boosts: " + boosts.ToString();
	}
	
	public static void SetScore(float distance){
		instance.ScoreText.text = "Score: " + distance.ToString();

	}
}