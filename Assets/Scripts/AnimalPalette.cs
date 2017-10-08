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

	public ModelPlacer modelplacer;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
		tempoText.text = tempoSlider.value.ToString ();
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
		var newAlpha = opening ? 0f : 1f;
		var oldAlpha = opening ? 1f : 0f;
//		if (!opening) {
//			button_renderer.gameObject.SetActive (true);
//			button_renderer.SetAlpha (0f);
//		}
		var oldX = rt.anchoredPosition.x;
		while (t < openCloseDuration) {
			yield return null;
			t += Time.deltaTime;
			var delta = t / openCloseDuration;
			var x = Mathf.Lerp (oldX, destination, delta);
			rt.anchoredPosition = new Vector2 (x, rt.anchoredPosition.y);
			var a = Mathf.Lerp (oldAlpha, newAlpha, delta);
//			button_renderer.SetAlpha (a);
		}
//		if (opening) {
//			button_renderer.gameObject.SetActive (false);
//		}
	}

	public void changeBpmText(float val) {
		tempoText.text = val.ToString ();
	}

}
