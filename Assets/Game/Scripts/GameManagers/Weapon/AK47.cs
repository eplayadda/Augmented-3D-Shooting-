using UnityEngine;
using System.Collections;

public class AK47 : WeaponBaseClass
{
	void Start()
	{
		CreateBullets ();

	}
	void OnEnable()
	{
		bulletStartPoint=WeaponManager.Instance.bulletStartPointTransform.position;
		CrossHair.SetActive (true);
		CrossHairAlphaGameObj.SetActive (true);
		PlayerManager.Instance.UpdateRespectToGun (3000,50);
		AIManager.Instance.ScaleTheSizeOfGhost (.75f);

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
			CrossHair.SetActive (false);
			CrossHairAlphaGameObj.SetActive (false);

		}

	}
}
