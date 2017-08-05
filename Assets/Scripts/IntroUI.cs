using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour {

	public Text leftTitle,rightTitle,leftContent,rightContent,message;
	public GameObject back,next,done;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		leftTitle.text="Grab & Throw";
		leftContent.text="Grab objects with any trigger click. Throw them by releasing the trigger.";
		rightTitle.text="Collectables";
		rightContent.text="Collect all the stars to progress to next level";
		message.text="Hit ball on next to procced";
	}

	public void NextClick()
	{
		back.SetActive(true);
		next.SetActive(false);
		done.SetActive(true);
		leftTitle.text="Movement";
		leftContent.text="Press the left touchpad to activate teleportation laser. Release it to move on the location";
		rightTitle.text="Spawning";
		rightContent.text="Press the right touchpad to activate object menu. Swipe to change object. While holding the touchpad press the right trigger to spawn the displayed object.";
	}

	public void BackClick()
	{
		back.SetActive(false);
		next.SetActive(true);
		done.SetActive(false);
		leftTitle.text="Grab & Throw";
		leftContent.text="Grab objects with any trigger click. Throw them by releasing the trigger.";
		rightTitle.text="Collectables";
		rightContent.text="Collect all the stars to progress to next level";
	}

	public void DoneClick()
	{
		gameObject.SetActive(false);
	}
}
