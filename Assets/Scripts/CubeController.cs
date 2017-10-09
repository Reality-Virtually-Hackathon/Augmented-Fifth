using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour 
{

	private Renderer rend;
	public bool cubeActivated = false;
	public int cubeIndex;
	Color offColor, onColor1, onColor2, onColor3, onColor4, beatColor;
	private BeatHandler beat;
	public Sequencer sequencer;
	public bool mouseDown;
	public bool isPercussion;
	public int colorNum = 0;

	void Awake() {
		beat = BeatHandler.instance;
	}

	// Use this for initialization
	void Start () 
	{
		if (isPercussion){
			offColor = new Color();
			ColorUtility.TryParseHtmlString ("#FFFFFFFF", out offColor);
			onColor4 = new Color();
			ColorUtility.TryParseHtmlString ("#00FFCEFF", out onColor4);
			onColor3 = new Color();
			ColorUtility.TryParseHtmlString ("#00C3FFFF", out onColor3);
			onColor2 = new Color();
			ColorUtility.TryParseHtmlString ("#2727FFFF", out onColor2);
			onColor1 = new Color();
			ColorUtility.TryParseHtmlString ("#9500FFFF", out onColor1);
			beatColor = new Color();
			ColorUtility.TryParseHtmlString ("#00FF0000", out beatColor);
		}
		else{
			offColor = new Color();
			ColorUtility.TryParseHtmlString ("#FFFFFFFF", out offColor);
			onColor4 = new Color();
			ColorUtility.TryParseHtmlString ("#FFE000FF", out onColor4);
			onColor3 = new Color();
			ColorUtility.TryParseHtmlString ("#FF9D00FF", out onColor3);
			onColor2 = new Color();
			ColorUtility.TryParseHtmlString ("#FF4E00FF", out onColor2);
			onColor1 = new Color();
			ColorUtility.TryParseHtmlString ("#FF0062FF", out onColor1);
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
		colorNum = getColorNum();
		switchColor(colorNum);
	}

	void playSound() {
		if (sequencer.audible){
			if (beat.currBeat == cubeIndex){
				if (colorNum != 0){
					if (beat.randomMode){
						System.Random rnd = new System.Random();
						int rndSound = rnd.Next(0, sequencer.sounds.Length);
						sequencer.audio.PlayOneShot(sequencer.sounds[rndSound]);
					}
					else{
						sequencer.audio.PlayOneShot(sequencer.sounds[colorNum-1]);
					}
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
		switchColor(colorNum);
		yield return null;
    }

	int getColorNum(){
		if (colorNum == 4){
			colorNum = 0;
			cubeActivated = false;
		}
		else{
			colorNum++;
			cubeActivated = true;
		}
		return colorNum;
	}

	void switchColor(int colorNum){
		if (colorNum == 0){
			gameObject.GetComponent<Renderer>().material.color = offColor;
			}
		if (beat.randomMode){
			if (colorNum != 0){
				gameObject.GetComponent<Renderer>().material.color = onColor1;
			}
		}
		else{
			if (colorNum == 1){
				gameObject.GetComponent<Renderer>().material.color = onColor1;
			}
			if (colorNum == 2){
				gameObject.GetComponent<Renderer>().material.color = onColor2;
			}
			if (colorNum == 3){
				gameObject.GetComponent<Renderer>().material.color = onColor3;
			}
			if (colorNum == 4){
				gameObject.GetComponent<Renderer>().material.color = onColor4;
			}
		}
	}
}
