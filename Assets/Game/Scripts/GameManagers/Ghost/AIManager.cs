using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {
	
	public static AIManager Instance;
	
	public enum AIStates {
		
		kIdle = 0,
		kFly,
		kAttack,
		kDead,
		kStop,
		kNone
	}
	
	public enum AITypes {
		
		kType1
	}
	
	public GameObject[] aiTypeArryForSinglePlayer;
	public GameObject [] aiTypeArryForMultiPlayerPlayer;
	public ParticleSystem bloodEffect;
	public Transform cameraTransform;
	public Vector3 aiPosition;
	private GameObject go;
	private int thita;
	[HideInInspector] public int totalKillCount;
	public int totalNumberOfAI;
	[Range(90,1000)] public int ghostCreationMaxRange = 90;
	public Transform[] ghostCloneArray;
	public int[] layersOfENvornment;
	void Awake() {
		
		Instance=this;
	} 
	
	void Start ()
	{
		
		go = new GameObject();
		go.name = "Pool Of Ghost";
		ghostCloneArray=new Transform[totalNumberOfAI];
		if(! DontDestroyOnLoadClass.Instance.isMultiPlayer )
		{
			CreateAI(totalNumberOfAI);
		}	
		else
		{
			if(PhotonNetwork.isMasterClient )
			{
				CreateAIForMultiPlayer(totalNumberOfAI);
			}			
		}
//					ScaleTheSizeOfGhost(.5f);
	}
	
	void CreateAI (int noOfAi)
	{
		GameObject temp = null;
		for(int i = 0; i < noOfAi; i++) 
		{
			temp = Instantiate (aiTypeArryForSinglePlayer[0],GetRandomPoint(),Quaternion.identity) as GameObject;
			temp.GetComponent<AI>().current_Z_AngleOf_AI_WithWorld = thita;
			//			temp.name = "Ghost";
			temp.transform.parent = go.transform;			
			ghostCloneArray[i] = temp.transform;
		}
	}
	
	void CreateAIForMultiPlayer(int noOfAi)
	{
		GameObject temp = null;
		for(int i = 0; i < noOfAi; i++) 
		{
			temp = PhotonNetwork.Instantiate(aiTypeArryForMultiPlayerPlayer[0].name,GetRandomPoint(),Quaternion.identity,0) as GameObject;//Instantiate (aiTypeArry[0],GetRandomPoint(),Quaternion.identity) as GameObject;
			temp.GetComponent<AI>().current_Z_AngleOf_AI_WithWorld = thita;
			//			temp.name = "Ghost";
			temp.transform.parent = go.transform;			
			ghostCloneArray[i] = temp.transform;
		}
	}
	
	#region Helpers
	public Vector3 GetRandomPoint(Transform currentAiObj = null) 
	{
		int yAxisOfAI = Random.Range (120,ghostCreationMaxRange);
		thita = Random.Range (0,360);
		if(currentAiObj != null)
			currentAiObj.GetComponent<AI>().current_Z_AngleOf_AI_WithWorld = thita;
		float xAxis = yAxisOfAI * Mathf.Cos ((Mathf.PI/180) * thita);
		float yAxis = yAxisOfAI * Mathf.Sin ((Mathf.PI/180) * thita);
		return new Vector3(xAxis,yAxis,0); 
	}
	
	public Vector3 GetRandomPoint(int currentAngle,float distance,Transform currentAiObj)
	{
		//		int yAxisOfAi = (int)Random.Range (GetRange(distance,true),GetRange(distance,false));
		int yAxisOfAi = (int)GetNextTarget(distance);
		currentAngle = (int)Mathf.Pow (-1,Random.Range (0,2)) * 60 + currentAngle;
		currentAiObj.GetComponent<AI>().current_Z_AngleOf_AI_WithWorld = currentAngle;
		float xAxis = yAxisOfAi * Mathf.Cos ((Mathf.PI/180) * currentAngle);
		float yAxis = yAxisOfAi * Mathf.Sin ((Mathf.PI/180) * currentAngle);
		return new Vector3(xAxis,yAxis,0);
	}
	
	//	float GetRange(float currentDistance,bool isMinRange)
	//	{
	//		float rangeValue = 0;
	//		if(currentDistance < 100) 
	//		{
	//			if(isMinRange)
	//				rangeValue = 20;
	//			else
	//				rangeValue = 90;
	//		}
	//		else if(currentDistance < 500) 
	//		{
	//			if(isMinRange)
	//				rangeValue = 110;
	//			else
	//				rangeValue = 490;
	//		}
	//		else if(currentDistance > 500)
	//		{
	//			if(isMinRange)
	//				rangeValue = 510;
	//			else
	//				rangeValue = 990;
	//		}
	//		return rangeValue;
	//	}
	float GetNextTarget(float currentDistanceWithPlayer)
	{
		float pRange=0;
		if(currentDistanceWithPlayer<=layersOfENvornment[0])
		{
//			Handheld.Vibrate ();
			PlayerManager.Instance.UpdatePlayerHealth (5);
			if(Random.Range (0,10)>3)
				pRange=layersOfENvornment[Random.Range (1,layersOfENvornment.Length )];
			else 
				pRange=25;
		}
		else
		{
			pRange=layersOfENvornment[0];
		}
		return pRange;
		
	}
	public void ScaleTheSizeOfGhost(float pScale)
	{
		for(int i=0;i<ghostCloneArray.Length;i++)
		{
			ghostCloneArray[i].localScale=new Vector3(pScale,pScale,pScale);
			
			
		}
	}
	#endregion
}
