using UnityEngine;
using System.Collections;

public class FilterButton : MonoBehaviour {

	public CatalogManager manager;
	public Sprite offSprite;
	public Sprite onSprite;
	public bool isOn = false;  // button status
	public bool roomFilterActive = false;
	public bool typeFilterActive = false;
	[Range(9, 27)]
	[Header("9-16 room filters; 17-27 type filters")]
	public int buttonID;
	public ObjectCategory.ROOMCODE buttonRoomCode;
	public ObjectCategory.OBJECTTYPE buttonTypeCode;

    public SteamVR_LaserPointer pointer;
    public SteamVR_TrackedController controller;

    public CatalogManager catalogManager;


    private UnityEngine.UI.Image buttonImage;
	private bool isSelected = false;
    private bool buttonSelected = false;
    private static ObjectCategory.ROOMCODE  filterRoomCode = ObjectCategory.ROOMCODE.ALL;
	private static ObjectCategory.OBJECTTYPE filterTypeCode = ObjectCategory.OBJECTTYPE.ALL;

	void OnEnable() {
		if(buttonImage == null) {
			buttonImage = GetComponent<UnityEngine.UI.Image>();
		}

		if (buttonID == 16) {
			filterRoomCode = ObjectCategory.ROOMCODE.ALL;
			//manager.FiltRoomCode = filterRoomCode;
			isOn = false;
			roomFilterActive = true;
			isSelected = false;
			turnOn();
		}
		else if (buttonID == 26) {
			filterTypeCode = ObjectCategory.OBJECTTYPE.ALL;
			//manager.FiltTypeCode = filterTypeCode;
			isOn = false;
			typeFilterActive = true;
			isSelected = false;
			turnOn();
		}
		else {
			isOn = true;
			roomFilterActive = false;
			typeFilterActive = false;
			isSelected = false;
			turnOff();
		}
		
	}


	void Start() {
		if (buttonImage == null) {
			buttonImage = GetComponent<UnityEngine.UI.Image>();
		}

        pointer.PointerIn += OnObjectHover;
        pointer.PointerOut += OffObjectHover;
    }

    void LateUpdate()
    {
        if (buttonID < 17)
        {
            if (isOn && filterRoomCode != buttonRoomCode && !isSelected)
            {
                roomFilterActive = false;
                turnOff();
            }
        }
        else
        {
            if (isOn && filterTypeCode != buttonTypeCode && !isSelected)
            {
                typeFilterActive = false;
                turnOff();
            }
        }

    }

    public virtual void OnObjectHover(object sender, PointerEventArgs e)
    {
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
            isSelected = false;

            if (buttonID < 17)
            {
                if (!roomFilterActive)
                    roomFilterActive = true;
                else
                    return;
            }
            else
            {
                if (!typeFilterActive)
                    typeFilterActive = true;
                else
                    return;
            }



            switch (buttonID)
            {

                // cases for room filters
                case 9:
                    filterRoomCode = ObjectCategory.ROOMCODE.KITCHEN;
                    manager.FiltRoomCode = ObjectCategory.ROOMCODE.KITCHEN;
                    roomFilterActive = true;
                    break;
                case 10:
                    filterRoomCode = ObjectCategory.ROOMCODE.BATHROOM;
                    manager.FiltRoomCode = ObjectCategory.ROOMCODE.BATHROOM;
                    roomFilterActive = true;
                    break;
                case 11:
                    filterRoomCode = ObjectCategory.ROOMCODE.BEDROOM;
                    manager.FiltRoomCode = ObjectCategory.ROOMCODE.BEDROOM;
                    roomFilterActive = true;
                    break;
                case 12:
                    filterRoomCode = ObjectCategory.ROOMCODE.LIVING;
                    manager.FiltRoomCode = ObjectCategory.ROOMCODE.LIVING;
                    roomFilterActive = true;
                    break;
                case 13:
                    filterRoomCode = ObjectCategory.ROOMCODE.DINING;
                    manager.FiltRoomCode = ObjectCategory.ROOMCODE.DINING;
                    roomFilterActive = true;
                    break;
                case 14:
                    filterRoomCode = ObjectCategory.ROOMCODE.REC;
                    manager.FiltRoomCode = ObjectCategory.ROOMCODE.REC;
                    roomFilterActive = true;
                    break;
                case 15:
                    filterRoomCode = ObjectCategory.ROOMCODE.OUTDOORS;
                    manager.FiltRoomCode = ObjectCategory.ROOMCODE.OUTDOORS;
                    roomFilterActive = true;
                    break;
                case 16:
                    filterRoomCode = ObjectCategory.ROOMCODE.ALL;
                    manager.FiltRoomCode = ObjectCategory.ROOMCODE.ALL;
                    roomFilterActive = true;
                    break;


                // cases for type filters
                case 17:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.APPLIANCE;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;
                case 18:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.COMFORT;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;
                case 19:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.DECORATION;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;
                case 20:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.ELECTRONIC;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;
                case 21:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.KIDS;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;
                case 22:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.LIGHTING;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;
                case 23:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.PLUMBING;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;
                case 24:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.SURFACES;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;
                case 25:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.MISC;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;
                case 26:
                    filterTypeCode = ObjectCategory.OBJECTTYPE.ALL;
                    manager.FiltTypeCode = filterTypeCode;
                    typeFilterActive = true;
                    break;

                default:
                    break;
            }
        }
    }

    public virtual void OffObjectHover(object sender, PointerEventArgs e)
    {
        if ((e.target == GetComponent<Collider>().transform))
        {
            controller.TriggerClicked -= OnSelectClick;
            isSelected = false;
            buttonSelected = false;
            turnOff();
        }
    }


	// sets the off sprite and flag
	public void turnOff() {
		if (!isOn)
			return;

		if (roomFilterActive || typeFilterActive)
			return;

		buttonImage.sprite = offSprite;
		isOn = false;
	}

	//sets the on sprite and flag
	public void turnOn() {
		if (isOn)
			return;

		buttonImage.sprite = onSprite;
		isOn = true;
	}


}
