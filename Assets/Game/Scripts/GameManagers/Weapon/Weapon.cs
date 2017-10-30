using UnityEngine;
using System.Collections;

public class Weapon : WeaponBaseClass
{
	void Start()
	{
		CreateBullets ();
	}
	void OnEnable()
	{
		CrossHair.SetActive (true);
		PlayerManager.Instance.UpdateRespectToGun (3000,60);
		AIManager.Instance.ScaleTheSizeOfGhost (.5f);
		bulletStartPoint=WeaponManager.Instance.bulletStartPointTransform.position;
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
			CrossHair.SetActive (false);

	}

}
