using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatHandler : MonoBehaviour {

	public float tempo;
	public int currBeat;
	public int numBeats;
	public delegate void BeatAction();
    public static event BeatAction OnBeat;
	public bool getNextBeat;
	public static BeatHandler instance;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		tempo = 60.0F;
		numBeats = 8;
		getNextBeat = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (getNextBeat){
			StartCoroutine("moveToNextBeat");
			if (OnBeat != null) {
				OnBeat();
			}
		}
	}
	

//	void OnGUI() {
//        tempo = GUI.VerticalSlider(new Rect(25, 25, 30, 100), tempo, 40.0F, 120.0F);
//    }

	IEnumerator moveToNextBeat() {
		getNextBeat = false;
		if (currBeat == numBeats){
			currBeat = 1;
		}
		else{
			currBeat++;
		}
		print(currBeat);
        yield return new WaitForSeconds(60.0F/tempo/4);
		getNextBeat = true;
    }

	public void onSliderChange(float val) {
		tempo = val;
//		Debug.Log ("value from the slider is " + val);
	}
}
