using UnityEngine;
using System.Collections;

public class GhostNetwork : Photon.MonoBehaviour {
	private AIBase aiRefrance;
	void Awake()
	{            
		
		aiRefrance = gameObject.GetComponent<AI>();
		gameObject.name = gameObject.name + photonView.viewID;
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		Debug.Log ("Data transfa2r");
		if (stream.isWriting)
		{
			//We own this player: send the others our data
			//			stream.SendNext((int)controllerScript._characterState);
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation); 
			stream.SendNext (aiRefrance.health);
			Debug.Log ("Send Data trans"+transform.position);
			
		}
		else
		{
			//Network player, receive data
			//			controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
			correctPlayerHealth = (float )stream.ReceiveNext ();
			Debug.Log ("Send Data Health"+correctPlayerHealth);

		}
	}
	
	private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
	private float correctPlayerHealth=100;
	void Update()
	{
		if (!photonView.isMine)
		{
			//Update remote player (smooth this, this looks good, at the cost of some accuracy)
			transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5f);
			transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
			HUDManager.Instance.UpdatePlayerHealth (correctPlayerHealth);

		}
	}
}
