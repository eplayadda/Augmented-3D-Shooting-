using UnityEngine;
using System.Collections;

public class Bullets1 : MonoBehaviour {
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
	public float speedIncrimentfactor;
	public float bulletRange;
	private Vector3 targetOfBullet;
	private Vector3 startPoint;
	private float timeForDisable;
	private RaycastHit rayHitObject;
	private WeaponManager weaponManagerObj;
	private Transform mainCamera;
	private bool isGhostHited=false;
	private float distanceBtnTargetNstart;
	public float intencity;
	 float journeyTime = 6f;
	private float startTime;
	private Vector3 centerOfProjectile;
	int layerOfEnornment=1;
	public 	float bulletSpeedLocal;
	void OnEnable () 
	{
		bulletSpeedLocal=speedOfBullets;
		eCurrentBulletStatus=eBulletStatus.kMoving;
		weaponManagerObj=WeaponManager.Instance;
		mainCamera=weaponManagerObj.mainCamera;
		startPoint=weaponManagerObj.bulletStartPointTransform.position;
		transform.position=startPoint;
		targetOfBullet=Camera.main.ScreenToWorldPoint (new Vector3(Screen.width/2,Screen.height/2,bulletRange));	
		startTime = Time.time;
		centerOfProjectile=mainCamera.up;
		if(Physics.Raycast (mainCamera.position,mainCamera.forward ,out rayHitObject,bulletRange))
		{
			if(rayHitObject.collider.CompareTag ("Ghost"))
			{
				rayHitObject.collider.gameObject.GetComponent<AIBase>().changeState (AIManager.AIStates.kStop);
				targetOfBullet=rayHitObject.collider.transform.position;
				isGhostHited=true;
				Debug.Log ("Hit");
			}
		}
//		if(transform.parent.parent.name.CompareTo("ShortGun") == 0)
//			distanceBtnTargetNstart=0.3f;
//		else
//			distanceBtnTargetNstart=Vector3.Distance (startPoint,targetOfBullet)/4;

		eCurrentBulletStatus=eBulletStatus.kMoving;
// 		timeForDisable = Vector3.Distance (startPoint,targetOfBullet)/ speedOfBullets*Time.deltaTime;
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
					float pDistanceBtnBulletAndPlayer=Vector3.Distance (transform.position,mainCamera.transform.position);
					if(pDistanceBtnBulletAndPlayer>25*layerOfEnornment)
					{
						layerOfEnornment+=1;
						IncressCollider (2*layerOfEnornment);
//						speedOfBullets+=speedOfBullets;
					}
					bulletSpeedLocal+=speedIncrimentfactor;
					if(pDistanceBtnBulletAndPlayer>=Vector3.Distance (startPoint,targetOfBullet)*.7f)
					transform.GetComponent<Renderer>().enabled=false;
		
					if(Vector3.Distance (transform.position,targetOfBullet)<.3f)
						StartCoroutine ("DisableBulletAtEnd");
				//						ShootTheGhost();
				}
				break;
				case eTypeOfBulletPath.Projectile:
				{
						Vector3 center = (startPoint + targetOfBullet) * 0.5F;
						center -= 300*centerOfProjectile;
						Vector3 riseRelCenter = startPoint - center;
						Vector3 setRelCenter = targetOfBullet - center;
						float fracComplete = (Time.time - startTime) / journeyTime;
						transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
						transform.position += center;
				}
				break;

			}
		}
	}
	IEnumerator DisableBulletAtEnd()
	{
		yield return new WaitForSeconds(.2f);
			gameObject.SetActive (false);
	}

	void OnDisable()
	{
		startTime = Time.time;
		eCurrentBulletStatus=eBulletStatus.kStop;
		isGhostHited=false;
		layerOfEnornment=1;
		gameObject.GetComponent<SphereCollider>().radius=.5f;
		transform.GetComponent<Renderer>().enabled=true;

	}
	void IncressCollider(float multipale)
	{
		gameObject.GetComponent<SphereCollider>().radius=multipale;
	}
	void ShootTheGhost()
	{
		if(isGhostHited )
			rayHitObject.collider.GetComponent<AIBase>().AiKilled (intencity);
		gameObject.SetActive (false);


	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.CompareTag("Ghost"))
		{
			Debug.Log ("AI Hit");
 			other.gameObject.GetComponent<AIBase>().AiKilled (intencity);
			StopCoroutine ("DisableBulletAtEnd");
			gameObject.SetActive (false);
		}
	}
}
