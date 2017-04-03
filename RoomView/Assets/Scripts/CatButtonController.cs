using UnityEngine;
using System.Collections;

public class CatButtonController : MonoBehaviour {

	public Sprite offSprite;
	public Sprite onSprite;
	public GameObject Preview;
	public CatalogManager catalogManager;
	[Range(1, 8)]
	[Header("1-6 = cat buttons; 7 = back, 8 = forward")]
	public int buttonID; 

	private bool isOn = false;  // button status
    private bool buttonSelected = false;
	private UnityEngine.UI.Image backImage;
	private bool isSelected = false;

    public SteamVR_LaserPointer pointer;
    public SteamVR_TrackedController controller;


    // Use this for initialization
    void Start () {
		backImage = GetComponent<UnityEngine.UI.Image>();

        pointer.PointerIn += OnObjectHover;
        pointer.PointerOut += OffObjectHover;
    }
	
	// Update is called once per frame
	void Update () {

	}

	public virtual void OnObjectHover(object sender, PointerEventArgs e) {
        if ((e.target == GetComponent<Collider>().transform))
        {
            controller.TriggerClicked += OnSelectClick;
            isSelected = true;
            buttonSelected = true;
            turnOn();
        }
	}

    public virtual void OnSelectClick(object sender, ClickedEventArgs e)
    {
        if (buttonSelected)
        {
            //isSelected = false;
            //buttonSelected = false;
            //turnOff();


            if (buttonID <= 6)
            {
                catalogManager.spawnByCatalogButton(buttonID, new Vector3(0, -100));
                isSelected = false;
                buttonSelected = false;
                turnOff();
                catalogManager.catOff();
            }
            else
            {
                if (buttonID == 7)
                    catalogManager.scrollBackward();
                else
                    catalogManager.scrollForward();
            }
        }
    }

    public virtual void OffObjectHover(object sender, PointerEventArgs e) {
        if ((e.target == GetComponent<Collider>().transform))
        {
            controller.TriggerClicked -= OnSelectClick;
            isSelected = false;
            buttonSelected = false;
            turnOff();
        }
	}
	void OnMouseDown() {
		isSelected = false;
		turnOff();

		if (buttonID <= 6)
			catalogManager.spawnByCatalogButton(buttonID, transform.position);
		else {
			if (buttonID == 7)
				catalogManager.scrollBackward();
			else
				catalogManager.scrollForward();
		}
	}

	// sets the off sprite and flag
	public void turnOff() {
		if (!isOn)
			return;

		backImage.sprite = offSprite;
		isOn = false;
	}

	//sets the on sprite and flag
	public void turnOn() {
		if (isOn)
			return;

		backImage.sprite = onSprite;
		isOn = true;
	}

	// changes the sprite according to status
	public void swapSprite() {
		if (isOn) {
			backImage.sprite = offSprite;
			isOn = false;
		}
		else {
			backImage.sprite = onSprite;
			isOn = true;
		}
	}

}
