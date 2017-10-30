using UnityEngine;
using System.Collections;

public class AI : AIBase 
{
	Vector3 targetPoint;
	
	void Start () 
	{
		if(PhotonNetwork.isMasterClient  || !DontDestroyOnLoadClass.Instance.isMultiPlayer)
		{
			this.movementSpeed = Random.Range (3,7);
			player = GameObject.Find ("MainCamera") ;
			transform.LookAt (player.transform.position,-Vector3.back);
			lastAttackTime =Time.time;
			SetPath ();
		}
		
	}
	
	protected override void Update () 
	{			
		base.Update();
		
		if(this.currentState == AIManager.AIStates.kIdle) {
			
			this.currentState = AIManager.AIStates.kFly;
		}
		else if(this.currentState == AIManager.AIStates.kFly) {
			if(PhotonNetwork.isMasterClient  || !DontDestroyOnLoadClass.Instance.isMultiPlayer)
				MoveTheGhost(targetPoint);
		}
		else if(this.currentState == AIManager.AIStates.kDead) {
			if(PhotonNetwork.isMasterClient  || !DontDestroyOnLoadClass.Instance.isMultiPlayer)
				MoveDownTheGhost();
		}
	}
	
	public override void AiKilled (float gunIntencity)
	{
		base.AiKilled (gunIntencity);

	}
	void MoveTheGhost(Vector3 pTargetPoint)
	{
		if(Vector3.Distance (transform.position,pTargetPoint) > .01f)
		{
			transform.position = Vector3.MoveTowards (transform.position,targetPoint,movementSpeed * Time.deltaTime);
			transform.LookAt (player.transform.position,-Vector3.back);
			
		}
		else 
		{
			SetPath ();
		}
	}
	
	void MoveDownTheGhost()
	{ 
		if(transform.position.z < ghostDiePoint.z)
		{
			transform.Rotate (new Vector3 (0, 0, Random.Range (0, 360)));
			transform.position = Vector3.MoveTowards (transform.position,ghostDiePoint,30*Time.deltaTime);
		}
		else
		{
			this.currentState = AIManager.AIStates.kFly;
		}
	}
	
	protected override void MakeAliveAi()
	{
		transform.position = AIManager.Instance.GetRandomPoint(transform);
		transform.LookAt (player.transform.position,-Vector3.back);
		base.MakeAliveAi();
		SetPath ();
	}
	
	void SetPath()
	{
		targetPoint = AIManager.Instance.GetRandomPoint (current_Z_AngleOf_AI_WithWorld,distanceWithPlayer,transform);
		
		if(distanceWithPlayer < 100)
			targetPoint = new Vector3(targetPoint.x,targetPoint.y,Random.Range (-5,5));
	}
}
