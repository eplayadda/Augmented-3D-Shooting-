using UnityEngine;
using System.Collections;

public class Sniper : WeaponBaseClass 
{
	void Start()
	{
		CreateBullets ();

	}
	void OnEnable()
	{
		bulletStartPoint=WeaponManager.Instance.bulletStartPointTransform.position;
		PlayerManager.Instance.UpdateRespectToGun (3000,40);
		AIManager.Instance.ScaleTheSizeOfGhost (1f);

		CrossHair.SetActive (true);
		CrossHairAlphaGameObj.SetActive (true);

	}
	public override void FireTheGun() 
	{
		if((Time.time -lastFiredTym)>recoilingTime) 
		{
			lastFiredTym=Time.time;
			base.FireTheGun ();
		}
	}
	void  OnDisable()
	{
		if(CrossHair  != null)
		{
			CrossHairAlphaGameObj.SetActive (false);
			CrossHair.SetActive (false);

		}

	}

}
