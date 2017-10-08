using UnityEngine;
using System.Collections;

public class Sequencer : MonoBehaviour
{

	public GameObject[] cubes;
	public AudioClip[] sounds;
	public AudioSource audio;
	public bool visible;
	public bool audible;
	// Use this for initialization
	void Start () 
	{
		visible = true;
		audible = true;
	}
	// Update is called once per frame
	void Update () 
	{
	}

}
