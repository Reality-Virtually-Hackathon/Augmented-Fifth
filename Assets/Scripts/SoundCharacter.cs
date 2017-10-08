using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCharacter : MonoBehaviour {

    Animator anim;

    protected float DefaultAnimationSpeed = 1f;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        SetAnimationSpeed(DefaultAnimationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // temp for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }
    }

    public void SetAnimationSpeed(float value)
    {
        anim.SetFloat("Speed", value);
    }

    public void Play()
    {
        // start the play animation
        anim.SetTrigger("Play");
    }
}
