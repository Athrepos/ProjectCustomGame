using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void finishAnimationHandler(object source, EventArgs e);
public class AnimationEventTrigger : MonoBehaviour {

	public event finishAnimationHandler finishAnim;


	// Use this for initialization
	void Start () {
		// existing components on the GameObject
		AnimationClip[] clips;
		Animator anim;

		// new event created
		AnimationEvent evt;
		evt = new AnimationEvent();

		// put some parameters on the AnimationEvent
		//  - call the function called PrintEvent()
		//  - the animation on this object lasts 2 seconds
		//    and the new animation created here is
		//    set up to happens 1.3s into the animation		
		evt.intParameter = 12345;

		evt.functionName = "PrintEvent";

		// get the animation clip and add the AnimationEvent
		anim = GetComponent<Animator>();
		clips = anim.runtimeAnimatorController.animationClips;
		evt.time = clips[0].length;

		foreach (var clip in clips) {
			clip.AddEvent(evt);
		}

	}
	
	public void PrintEvent(int i){
		OnAnimationFinish (new EventArgs());
	}


	protected virtual void OnAnimationFinish(EventArgs e)
	{
		//show the message to the view

		if (finishAnim != null)
			finishAnim (this, e);
	}
}
