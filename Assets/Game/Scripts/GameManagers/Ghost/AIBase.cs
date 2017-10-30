using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour {
	
	public AIManager.AIStates currentState;
	public AIManager.AIStates prevState;
	
	public Animation animationController;
	
	public GameObject player;
	
	public float movementSpeed;
	public float distanceWithPlayer;
	public float health = 100;
	public GameObject bullet;
	public int current_Z_AngleOf_AI_WithWorld;
	public float minimumDistaceOfAttack;
	public float timeIntervalForAttack;
	protected float lastAttackTime;
	protected Vector3 ghostDiePoint;
	protected virtual void Update() {
		if(PhotonNetwork.isMasterClient  || !DontDestroyOnLoadClass.Instance.isMultiPlayer)
			distanceWithPlayer = Vector3.Distance(player.transform.position,transform.position);
		
		if(currentState != prevState) {
			
			prevState = currentState;
			UpdateAnimation();
			UpdateBehavior();
		}
	}
	
	void UpdateAnimation() {
		
		switch(currentState) { 
			
		case AIManager.AIStates.kFly:
			animationController.Play("Fly"); 
			break;
			
		case AIManager.AIStates.kDead:
			animationController.Play("Die"); 
			break;
		}
	}
	
	void UpdateBehavior() {
		
		switch(currentState) {
			
		case AIManager.AIStates.kFly:
			if(health <= 0)
				MakeAliveAi();
			break;
		}
		
	}
	
	public virtual void AiKilled (float gunIntencity )
	{
//		Handheld.Vibrate ();
		AIManager.Instance.bloodEffect.Stop ();
		AIManager.Instance.bloodEffect.Play ();
		
		health -= gunIntencity;
		currentState = AIManager.AIStates.kFly;
		
		if((health) <= 0)
		{
			AIManager.Instance.totalKillCount++;
			HUDManager.Instance.UpdateGhostKillCount(AIManager.Instance.totalKillCount);
			currentState = AIManager.AIStates.kDead; 
			transform.GetComponent<BoxCollider>().enabled = false; 
			ghostDiePoint  = new Vector3(transform.position.x,transform.position.y,150f);
		}
		
	}
	
	protected virtual void MakeAliveAi()
	{
		health = 100;
		transform.localScale = new Vector3(1,1,1);
		transform.GetComponent<BoxCollider>().enabled = true; 
	}
	public  void changeState(AIManager.AIStates pChangeTo )
	{
		currentState=pChangeTo;
	}
	
}
