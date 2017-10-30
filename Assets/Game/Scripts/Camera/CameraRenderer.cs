using UnityEngine;
using System.Collections;

public class CameraRenderer : MonoBehaviour {
	WebCamTexture webcamTexture ;
	void Start () {
		WebCamTexture webcamTexture = null;
#if  UNITY_IOS
		 webcamTexture = new WebCamTexture(Screen.width ,Screen.height ,30);
#endif
		  
#if UNITY_ANDROID
		 webcamTexture = new WebCamTexture();
#endif

		Renderer renderer = gameObject.GetComponent<Renderer>();
		renderer.material.mainTexture = webcamTexture;
		Screen.sleepTimeout=SleepTimeout.NeverSleep;

#if  UNITY_IOS
		transform.localScale=new Vector3(transform.localScale.x,transform.localScale.y,-1*transform.localScale.z);
#endif

		#if  !UNITY_EDITOR
			webcamTexture.Play();
		#endif
	}
}
