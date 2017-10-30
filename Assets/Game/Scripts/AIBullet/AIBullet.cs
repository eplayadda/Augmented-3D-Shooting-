using UnityEngine;
using System.Collections;

public class AIBullet : MonoBehaviour
{
	public enum eTypeOfBulletPath
	{
		Straight,
		Projectile
	};
	public enum eBulletStatus
	{
		kMoving=1,
		kStop=0
	}
	public eBulletStatus eCurrentBulletStatus;
	public eTypeOfBulletPath emTypeOfBulletPath;
	
	public float speedOfBullets;
	public float bulletRange;
	private Vector3 targetOfBullet;
	private Vector3 startPoint;
	private float timeForDisable;
//	private RaycastHit rayHitObject;
//	private WeaponManager weaponManagerObj;
	private Transform mainCamera;
	public float intencity;
	float journeyTime = 6f;
	private float startTime;
	private Vector3 centerOfProjectile;
	void OnEnable () 
	{
		mainCamera=AIManager.Instance.cameraTransform;
		eCurrentBulletStatus=eBulletStatus.kMoving;
		startPoint=AIManager.Instance.aiPosition;
		transform.position=startPoint;
		targetOfBullet=AIManager.Instance.cameraTransform.position;
		startTime = Time.time;
		centerOfProjectile=mainCamera.up;

		eCurrentBulletStatus=eBulletStatus.kMoving;
		timeForDisable = Vector3.Distance (startPoint,targetOfBullet)/ speedOfBullets*Time.deltaTime;
		StartCoroutine ("DisableBulletAtEnd");
	}
	void Update () 
	{
		if(eCurrentBulletStatus == eBulletStatus.kMoving)
		{
			switch(emTypeOfBulletPath)
			{
			case eTypeOfBulletPath.Straight:
			{
				transform.position = Vector3.MoveTowards (transform.position,targetOfBullet,speedOfBullets*Time.deltaTime);
			}
				break;
			case eTypeOfBulletPath.Projectile:
			{
				Vector3 center = (startPoint + targetOfBullet) * 0.5F;
				center -= 50*centerOfProjectile;
				Vector3 riseRelCenter = startPoint - center;
				Vector3 setRelCenter = targetOfBullet - center;
				float fracComplete = (Time.time - startTime) / speedOfBullets;
				transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
				transform.position += center;
			}
				break;
				
			}
		}
	}
	IEnumerator DisableBulletAtEnd()
	{
		yield return new WaitForSeconds(7);
		gameObject.SetActive (false);
	}
	
	void OnDisable()
	{
		startTime = Time.time;
		eCurrentBulletStatus=eBulletStatus.kStop;
		
	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.CompareTag("MainCamera"))
		{
			PlayerManager.Instance.UpdatePlayerHealth (intencity);
			StopCoroutine ("DisableBulletAtEnd");
			gameObject.SetActive (false);
		}
	}
}
