using UnityEngine;
using System.Collections;

// This is the base class which holds the very basic details of the game..
public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public enum GameDifficulty {

		kEasy,
		kMedium,
		kHard
	}

	public enum GameMode {

		kSinglePlayer,
		kMultiplayer
	}

	public GameDifficulty currentGameDifficulty = GameDifficulty.kEasy;
	public GameMode currentGameMode = GameMode.kSinglePlayer;

	void Awake () {
			
		if (Instance == null)
		{
			DontDestroyOnLoad (this.gameObject);
			Instance = this;
		}
		else {
			
			DestroyImmediate (this.gameObject);
		}
	}
}
