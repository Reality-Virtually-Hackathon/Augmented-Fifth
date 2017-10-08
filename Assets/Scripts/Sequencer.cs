using UnityEngine;
using System.Collections;

public class Sequencer : MonoBehaviour
{

	public GameObject[] cubes;
	public AudioClip[] sounds;
	public AudioSource audio;
	public bool visible;
	public bool audible;
	public bool sequencerActivated = false;
	// Use this for initialization
	void Start () 
	{
		visible = true;
		audible = true;

	}
	// Update is called once per frame
	void Update () 
	{
		foreach(var cube in cubes) {
			var controller = cube.GetComponent<CubeController> ();
			if (controller.cubeActivated){
				sequencerActivated = true;
				return;
			}
		}
		sequencerActivated = false;
	}

}
