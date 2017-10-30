using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GyroscopePanning : MonoBehaviour
{
	void Start ()
	{
		Input.gyro.enabled = true;
		Input.gyro.updateInterval=.01f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	public void resetToNormal ()
	{
	}								

	public void startPanning ()
	{
		resetToNormal ();
		resumePanning ();
	}
	public void stopPanning ()
	{
		resetToNormal ();
		pausePanning ();
	}
	public void pausePanning ()
	{
		enabled = false;
	}
	public void resumePanning ()
	{
			enabled = true;
	}

	public  Quaternion GetRotFix()
	{
		if (Screen.orientation == ScreenOrientation.Portrait)
			return Quaternion.identity;
		if (Screen.orientation == ScreenOrientation.LandscapeLeft
		    || Screen.orientation == ScreenOrientation.Landscape)
			return Quaternion.Euler(0, 0, -90);
		if (Screen.orientation == ScreenOrientation.LandscapeRight)
			return Quaternion.Euler(0, 0, 90);
		if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
			return Quaternion.Euler(0, 0, 180);
		return Quaternion.identity;
	}

	public   Quaternion ConvertRotation(Quaternion q)
	{ 
		return new Quaternion(q.x,q.y,-q.z,-q.w);
	}

	  

}

