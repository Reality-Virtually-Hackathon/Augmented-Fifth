using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCharacter : MonoBehaviour {

    Animator anim;

    protected float DefaultAnimationSpeed = 1f;
	private float animLength = 1.25f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        SetAnimationSpeed(DefaultAnimationSpeed);
		BeatHandler.OnBeat += onBeat;
    }
		
    void Update()
    {
        /*
        // temp for testing
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q))
        {
            PlayAnimation();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayAnimation2();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAnimation();
            StopAnimation2();
        }
        */
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

    public void PlayAnimation2()
    {
        // start the play animation
        anim.SetBool("Playing2", true);
    }

    public void StopAnimation()
    {
        // stop the play animation
        anim.SetBool("Playing", false);
    }

    public void StopAnimation2()
    {
        // stop the play animation
        anim.SetBool("Playing2", false);
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
