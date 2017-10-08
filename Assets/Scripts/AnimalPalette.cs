using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalPalette : MonoBehaviour {

	private float openCloseDuration = 0.3f;
	private RectTransform rt;
	private bool isOpen = false;
	private Coroutine openCloseCo;

	public Text tempoText;
	public Slider tempoSlider;
	public Sprite hamburgerImg;
	public Sprite xImg;
	public Button stickerToggle;
	public Button randomToggle;

	public ModelPlacer modelplacer;
	private float randomOffAlpha = 0.25f;

	void Start () {
		rt = GetComponent<RectTransform> ();
		var initTempo = tempoSlider.value;
		BeatHandler.instance.tempo = initTempo;
		tempoText.text = initTempo.ToString ();
		setRandomMode (false);
	}

	private bool isSlow = false;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			toggle ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			Time.timeScale = isSlow ? 1f : 0.05f;
			isSlow = !isSlow;
		}
	}

	public void toggle(){
		if (openCloseCo != null) {
			StopCoroutine (openCloseCo);
		}
		openCloseCo = StartCoroutine (openCloseSequence (!isOpen));
		isOpen = !isOpen;
		modelplacer.setplacing (isOpen);
		stickerToggle.image.sprite = isOpen ? xImg : hamburgerImg;
	}

	IEnumerator openCloseSequence(bool opening) {
		var t = 0f;
		var destination = opening ? 0f : rt.rect.width;
		var oldX = rt.anchoredPosition.x;
		while (t < openCloseDuration) {
			yield return null;
			t += Time.deltaTime;
			var delta = t / openCloseDuration;
			var x = Mathf.Lerp (oldX, destination, delta);
			rt.anchoredPosition = new Vector2 (x, rt.anchoredPosition.y);
		}
	}

	public void changeBpm(float val) {
		tempoText.text = val.ToString ();
		BeatHandler.instance.tempo = val;
	}

	public void toggleRandom() {
		setRandomMode(!BeatHandler.instance.randomMode);
	}

	void setRandomMode(bool isOn) {
		BeatHandler.instance.randomMode = isOn;
		var color = randomToggle.image.color;
		var alpha = isOn ? 1f : randomOffAlpha;
		color.a = alpha;
		randomToggle.image.color = color;
	}

}
