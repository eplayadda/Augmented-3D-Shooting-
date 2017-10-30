using UnityEngine;
using System.Collections;

public class WeaponBaseClass : MonoBehaviour 
{


	public GameObject[] bulletPrefabsArr;

	public float recoilingTime;
	public  GameObject [] bulletsArray;
	public int totalBulletsInPool=30;
	public GameObject CrossHair;
	public GameObject CrossHairAlphaGameObj;
	public Camera mainCamera;
	protected GameObject bulletPool;
	protected float lastFiredTym;
	protected Vector3 bulletStartPoint;

	public virtual void FireTheGun ()
	{
		GameObject go = GetBullet ();
		if( go != null)
			go.SetActive (true);
	}
   	protected void CreateBullets()
	{
		GameObject bulletForThisGun = bulletPrefabsArr[0];
		bulletPool = new GameObject();
		bulletPool.name = "Bullet Pool";
		bulletPool.transform.parent  = transform;
		bulletsArray  = new GameObject[totalBulletsInPool];
		for(int i=0;i<totalBulletsInPool;i++)
		{
			GameObject go = Instantiate (bulletForThisGun) as GameObject;
			go.transform.parent  = bulletPool.transform;
			go.name = "Bullet";
			bulletsArray[i]=go;
		}
	}
	GameObject GetBullet()
	{
		foreach(GameObject obj in bulletsArray) {
			if(!obj.activeInHierarchy) {
				return obj;
			}
		}
		return null; 
	}

}
