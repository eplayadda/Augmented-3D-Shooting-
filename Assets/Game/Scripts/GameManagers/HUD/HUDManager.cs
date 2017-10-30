using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour {

	public static HUDManager Instance;

	public Text ghostKilledText;
	public Text playerHealthText;
	public GameObject gameOverPanel;
	void Awake() {

		Instance = this;
	}

	public void UpdateGhostKillCount(int value)
	{
		ghostKilledText.text = value.ToString();
	}

	public void UpdatePlayerHealth(float value)
	{
		if(value > 0)
		playerHealthText.text = value+"";
		else
			gameOverPanel.SetActive (true);
	}
}
