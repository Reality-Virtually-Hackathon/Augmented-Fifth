using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ModelPlacer : MonoBehaviour {

    public MyFocusSquare focusSquare;
    public GameObject modelPrefab_bird;
	public GameObject modelPrefab_manatee;
	public GameObject modelPrefab_lizard;
    public float objectHeight = 0.20f;
	public AnimalPalette animalMenu;

	public GameObject africanSequencerPrefab;
	public GameObject bellSequencerPrefab;
	public GameObject pianoSequencerPrefab;
	public GameObject potSequencerPrefab;
	public GameObject taikoSequencerPrefab;

	private bool isEnabled;
	private List<GameObject> characters = new List<GameObject>();

	public bool useInfinitePlanes;

	public void setplacing(bool active){
		focusSquare.gameObject.SetActive (active);
		isEnabled = active;
	}

	public void make_flamingo(){
		makeModel (focusSquare.foundSquare.transform.position, modelPrefab_bird, potSequencerPrefab);
	}

	public void make_manatee(){
		makeModel (focusSquare.foundSquare.transform.position, modelPrefab_manatee, africanSequencerPrefab);
	}

	public void make_lizard(){
		makeModel (focusSquare.foundSquare.transform.position, modelPrefab_lizard, bellSequencerPrefab);
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
//		foreach (var obj in characters) {
//			foreach (Transform child in obj.transform) {
//				if (child.CompareTag ("Sequencer")) {
//					child.LookAt (Camera.main.transform);
//					var turnAroundRot = Quaternion.AngleAxis (180f, Vector3.up);
//					child.transform.localRotation *= turnAroundRot;
//				} else if (child.CompareTag("Character")) {
//					var cameraWithZeroY = Camera.main.transform.position;
//					cameraWithZeroY.y = obj.transform.position.y;
//					child.transform.LookAt (cameraWithZeroY);
//				}
//			}
//		}
	}

    void makeModel(Vector3 pos, GameObject model, GameObject sequencer) {
		var parent = new GameObject ("character");
		parent.transform.position = pos;
		var y_angle = Camera.main.transform.rotation.eulerAngles.y + 180f;
		parent.transform.rotation = Quaternion.Euler (0, y_angle, 0);
		var charGo = Instantiate (model, parent.transform);
		var sequencerGo = Instantiate (sequencer, parent.transform);
		sizeObject(charGo);
		var character = charGo.GetComponent<SoundCharacter>();
		character.PlayAnimation ();
		animalMenu.toggle ();
		characters.Add (parent);
		// TODO select model and bring up audio menu
    }

    void sizeObject(GameObject go) {
		var bounds = new Bounds (go.transform.position, Vector3.zero);
		var renderers = go.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            bounds.Encapsulate(r.bounds);
        }
        var scaleFactor =  objectHeight / bounds.size.y;
		Debug.LogFormat("scale factor is {0} for the original height of {1} going down to {2}", scaleFactor, bounds.size.y, objectHeight);
        go.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
