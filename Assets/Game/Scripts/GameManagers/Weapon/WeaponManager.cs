using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour 
{
	public static WeaponManager Instance;
	public GameObject[] guns;
	public GameObject [] bulletType;
	public Transform bulletStartPointTransform;
	public Transform mainCamera;
	private int currentActiveGun;
	private GameObject currentGunActive;

	void Awake()
	{

		Instance = this;  
	}

	void OnEnable()
	{

		EventManager.OnTouchDown += FireCurrentGun;
	}

	void OnDisable()
	{

		EventManager.OnTouchDown -= FireCurrentGun;
	}

	void Start () 
	{

		CreateGunForUse(0);
	}

	public void FireCurrentGun()
	{

		if(guns[currentActiveGun].activeInHierarchy)
		{

			guns[currentActiveGun].GetComponent<WeaponBaseClass>().FireTheGun ();
		}
	}

	public  void CreateGunForUse(int gunIndex) 
	{
		currentActiveGun = gunIndex;
		for(int index = 0; index < guns.Length; index++)
		{
			if(index == gunIndex)
				guns[index].SetActive (true);
			else
				guns[index].SetActive (false);

		}
	}

}
