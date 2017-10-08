using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCharacter : MonoBehaviour {

    Animator anim;

    protected float DefaultAnimationSpeed = 1f;
	private float animLength = 1.25f;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        SetAnimationSpeed(DefaultAnimationSpeed);
		BeatHandler.OnBeat += onBeat;
    }

    // Update is called once per frame
    void Update()
    {
        // temp for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAnimation();
        }
    }

    public void SetAnimationSpeed(float value)
    {
        anim.SetFloat("Speed", value);
    }

    public void PlayAnimation()
    {
        // start the play animation
        anim.SetBool("Playing", true);
    }

    public void StopAnimation()
    {
        // stop the play animation
        anim.SetBool("Playing", false);
    }

	void onBeat() {
		// TODO implement
		bool hasSoundOnNextBeat = true; // get if the this character has a sound for the next beat
		if (hasSoundOnNextBeat) {
			StartCoroutine (beatAnimation ());
		}
	}

	IEnumerator beatAnimation() {
		var tempo = BeatHandler.instance.tempo;
		var secsPerBeat = 60f / tempo / 4f;
		var speed = (1f / secsPerBeat) / animLength;
		SetAnimationSpeed (speed);
		yield return new WaitForSeconds (secsPerBeat / 2f);
		PlayAnimation ();
	}
}
