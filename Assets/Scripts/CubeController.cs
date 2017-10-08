using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour 
{

	private Renderer rend;
	public bool state = false;
	public int cubeIndex;
	Color offColor, onColor, beatColor;
	private BeatHandler beat;
	public Sequencer sequencer;
	public bool mouseDown;
	public bool isPercussion;

	void Awake() {
		beat = BeatHandler.instance;
	}

	// Use this for initialization
	void Start () 
	{
		if (isPercussion){
			offColor = new Color();
			ColorUtility.TryParseHtmlString ("#FF8400FF", out offColor);
			onColor = new Color();
			ColorUtility.TryParseHtmlString ("#FF0079FF", out onColor);
			beatColor = new Color();
			ColorUtility.TryParseHtmlString ("#00FF0000", out beatColor);
		}
		else{
			offColor = new Color();
			ColorUtility.TryParseHtmlString ("#2C5BB6FF", out offColor);
			onColor = new Color();
			ColorUtility.TryParseHtmlString ("#00F4FFFF", out onColor);
			beatColor = new Color();
			ColorUtility.TryParseHtmlString ("#00FF0000", out beatColor);
		}

		string cubeName = gameObject.name;
		int.TryParse(cubeName.Substring(cubeName.IndexOf("(")+1, 1), out cubeIndex);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!sequencer.visible){
			gameObject.GetComponent<Renderer>().enabled = false;
		}
		else{
			gameObject.GetComponent<Renderer>().enabled = true;
		}
	}

	void OnEnable()
    {
        BeatHandler.OnBeat += playSound;
		BeatHandler.OnBeat += pulseCube;
    }

	void OnDisable()
    {
        BeatHandler.OnBeat -= playSound;
		BeatHandler.OnBeat -= pulseCube;
    }

	void OnMouseDown()
	{
		if(state == false)
		{
			gameObject.GetComponent<Renderer>().material.color = onColor;
		}
		else
			gameObject.GetComponent<Renderer>().material.color = offColor;

		state =! state;
	}

	void playSound() {
		if (sequencer.audible){
			if (beat.currBeat == cubeIndex){
				if (state){
					System.Random rnd = new System.Random();
					int rndSound = rnd.Next(0, sequencer.sounds.Length);
					sequencer.audio.PlayOneShot(sequencer.sounds[rndSound]);
					//sequencer.audio.PlayOneShot(sequencer.sounds[cubeIndex]);
				}
			}
		}
	}

	void pulseCube() {
		StartCoroutine("pulseOverTime");
	}

	IEnumerator pulseOverTime() {
		if (beat.currBeat == cubeIndex){
			gameObject.GetComponent<Renderer>().material.color = beatColor;
		}
        yield return new WaitForSeconds(60.0F/beat.tempo/4);
		if (state){
			gameObject.GetComponent<Renderer>().material.color = onColor;
		}
		else{
			gameObject.GetComponent<Renderer>().material.color = offColor;
		}
		yield return null;
    }


}
