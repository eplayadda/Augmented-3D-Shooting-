using UnityEngine;
using System.Collections;

// In this class we are handling events like touch inputs, Orientation etc..
public class EventManager : MonoBehaviour {

	public delegate void TouchHandlerDelegate();
	public static event TouchHandlerDelegate OnTouchDown;

	void Awake() {

		DontDestroyOnLoad(gameObject);		
	}

	public void FireButtonClicked()
	{
		OnTouchDown();
	}
}
