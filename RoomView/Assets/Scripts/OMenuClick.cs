using UnityEngine;
using System.Collections;

public class OMenuClick : MonoBehaviour {

	private UnityEngine.UI.Image activeImage;

	private bool isOn = false;	// button status

    public SteamVR_TrackedObject controller;
	public Sprite onSprite;		// holds the on sprite
	public Sprite offSprite;	// holds the off sprite
    private SteamVR_LaserPointer pointer;


    // Use this for initialization
    void Start () {
		activeImage = GetComponent<UnityEngine.UI.Image>(); // get the image component
        pointer = controller.GetComponent<SteamVR_LaserPointer>();

 
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	// changes the sprite according to status
	public void swapSprite(){
		if (isOn){
			activeImage.sprite = offSprite;
			isOn = false;
		}
		else{
			activeImage.sprite = onSprite;
			isOn = true;
		}
	}

	// sets the off sprite and flag
	public void turnOff() {
		if (!isOn)
			return;

		activeImage.sprite = offSprite;
		isOn = false;
	}

	//sets the on sprite and flag
	public void turnOn() {
		if (isOn)
			return;

		activeImage.sprite = onSprite;
		isOn = true;
	}
}
