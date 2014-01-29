using UnityEngine;

public class GUIManager : MonoBehaviour {

	public GUIText gameOverText, instructionsText, runnerText, scoreText, powerUpsText;

	// creating an instance which is necessary to call non-static methods
	private static GUIManager instance;		
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
		scoreText.enabled = false;
		powerUpsText.enabled = false;
	}

	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		scoreText.enabled = true;
		powerUpsText.enabled = true;
		enabled = false;
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
	}

	// setters for Runner to send info to update the GUI
	public static void SetPowerUps(int powerUps){		
		instance.powerUpsText.text = "Power Ups: " + powerUps.ToString();
	}
	
	public static void SetDistance(float distance){
		instance.scoreText.text = "Score: " + distance.ToString("f0");

	}
}