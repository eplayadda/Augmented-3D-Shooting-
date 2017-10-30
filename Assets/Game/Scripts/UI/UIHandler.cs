using UnityEngine;
using System.Collections;

public class UIHandler : MonoBehaviour 
{
	public Canvas canvasObj;
	void Start()
	{
//		canvasObj.scaleFactor=Screen.height/Screen.width;
	}
	public void PistolSelect()
	{
		WeaponManager.Instance.CreateGunForUse (0);
	}
	public void AK47Select()
	{
		WeaponManager.Instance.CreateGunForUse (1);
	}
	public void SniperSelect()
	{
		WeaponManager.Instance.CreateGunForUse (2);
	}
	public void RePlayGame()
	{
		Application.LoadLevel (0);
	}
}
