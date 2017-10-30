using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour 
{

	public static PlayerManager Instance = null; 
	public GyroscopePanning gyroScopeScript=null;
	public Camera mainCamera=null ;
	public  float health=100;
	private float cameraSlerpFactor;

	void Awake() 
	{
		Instance = this;
	}

	void UpdatePlayerRotation()
	{

	}

	void Update ()
	{
		transform.rotation = Quaternion.Slerp (transform.rotation,CurrentGyroAngle()*gyroScopeScript.GetRotFix(),cameraSlerpFactor*Time.deltaTime) ;

	}

	Quaternion CurrentGyroAngle()
	{	
		Quaternion gyroCurrentAngle= gyroScopeScript.ConvertRotation (Input.gyro.attitude);
			return gyroCurrentAngle;
	}



	public void UpdatePlayerHealth(float damageValue) 
	{
		health -= damageValue;
		HUDManager.Instance.UpdatePlayerHealth (health);
	
	}

	public void UpdateRespectToGun(int slrepFactor,int fieldViewAngle)
	{
		cameraSlerpFactor = slrepFactor;
		mainCamera.fieldOfView=fieldViewAngle;
	}

}
