using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ModelPlacer : MonoBehaviour {

    public MyFocusSquare focusSquare;
    public GameObject modelPrefab;
    public float objectHeight = 0.20f;

	private bool isEnabled;

	public bool useInfinitePlanes;

	public void setplacing(bool active){
		focusSquare.gameObject.SetActive (active);
		isEnabled = active;
	}

	// Use this for initialization
	void Start () {
		focusSquare.gameObject.SetActive (false);
		setplacing (false);
		if (useInfinitePlanes) {
			UnityARUtility.planeScalingFactor = 1f;
			focusSquare.useProjectedPlanes = true;
		} else {
			UnityARUtility.planeScalingFactor = 0.1f;
			focusSquare.useProjectedPlanes = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isEnabled) {
			if (Input.touches.Length > 0) {
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began) {
					if (focusSquare.SquareState == MyFocusSquare.FocusState.Found) {
						makeModel(focusSquare.foundSquare.transform.position);
					}
				}
			}
		}
        if (Input.GetKeyDown(KeyCode.Space)) {
            makeModel(Vector3.forward);
        }
	}

    void makeModel(Vector3 pos) {
		var go = Instantiate (modelPrefab, pos, Quaternion.identity);
		sizeObject(go);
		var character = go.GetComponent<SoundCharacter>();
		character.Play ();
    }

    void sizeObject(GameObject go) {
        var bounds = new Bounds();
		var renderers = go.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            bounds.Encapsulate(r.bounds);
        }
        var scaleFactor =  objectHeight / bounds.size.y;
//        Debug.LogFormat("scale factor is {0} for a height of {1}", scaleFactor, bounds.size.y);
        go.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        go.transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
}
