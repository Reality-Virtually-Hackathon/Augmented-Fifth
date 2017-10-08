using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class InEditorCameraSetup : MonoBehaviour
{

#if UNITY_EDITOR
	void Awake ()
	{
		var arVideo = Camera.main.GetComponent<UnityARVideo> ();
		var cameraManager = Camera.main.GetComponent<UnityARCameraManager> ();
		arVideo.enabled = false;
		cameraManager.enabled = false;
		Camera.main.clearFlags = CameraClearFlags.Skybox;
	}
#endif
}
