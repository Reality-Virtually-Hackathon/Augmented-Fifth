using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCharacter : MonoBehaviour {

    Animator anim;

    protected float DefaultAnimationSpeed = 1f;
	private float animLength = 1.25f;
	private bool hasStarted = false;
	public Sequencer sequencer;

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
		if (BeatHandler.instance.currBeat == 1 && !hasStarted && sequencer.sequencerActivated) {
			StartCoroutine (beatAnimation ());
		}
	}

	IEnumerator beatAnimation() {
		hasStarted = true;
		var tempo = BeatHandler.instance.tempo;
		var secsPerBeat = 60f / tempo / 4f;
//		var timeTillStart = (BeatHandler.instance.numBeats - BeatHandler.instance.currBeat) * secsPerBeat;

		PlayAnimation ();
		while (true) {
			yield return null;
			// update speed
			if (!sequencer.sequencerActivated) {
				StopAnimation ();
				hasStarted = false;
			}
//			SetAnimationSpeed (speed);
		}
	}
}
