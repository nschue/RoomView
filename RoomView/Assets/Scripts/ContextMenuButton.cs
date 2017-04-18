using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContextMenuButton : MenuButton {


    public enum action
    {
        move, rotate, clone, del
    }

    public Sprite onSprite;     // holds the on sprite
    public Sprite offSprite;    // holds the off sprite
    public action selectedAction;

    private GameObject snappedObject;
    private static bool isContextMenu; 
    private bool isOn = false;	// button status
    private UnityEngine.UI.Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<UnityEngine.UI.Image>();
        controllerInput.TriggerClicked += OnTriggerCloseContextMenu;
        snappedObject = transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject;
    }

    // sets the off sprite and flag
    public void turnOff()
    {
        if (!isOn)
            return;

        buttonImage.sprite = offSprite;
        isOn = false;
    }

    //sets the on sprite and flag
    public void turnOn()
    {
        if (isOn)
            return;

        buttonImage.sprite = onSprite;
        isOn = true;
    }

    private void DeselectFurniture()
    {
        snappedObject.GetComponent<Furniture>().isSelected = false;
        Furniture.isFurnitureSelected = false;
    }

    private void MoveFurniture()
    {
        snappedObject.layer = 2;
        snappedObject.GetComponent<Furniture>().isMove = true;
    }

    private void RotateFurniture()
    {
        //Hides the context menu button while keeping it in the scene
        //This prevents event listener from being lost before the user is done rotating the object. 
        foreach(Transform child in transform.parent)
        {
            child.gameObject.SetActive(false);
        }
        controllerInput.PadClicked += OnPadClickedRotate;
        controllerInput.PadUnclicked += OnPadUnclickedStopRotate;
    }

    private void CloneFurniture()
    {
        DeselectFurniture();
        GameObject cloneObject = Instantiate(snappedObject);
        cloneObject.GetComponent<Furniture>().isClone = true;
        cloneObject.GetComponent<Furniture>().isSelected = true;
        cloneObject.GetComponent<Furniture>().fromCatalog = false;
        cloneObject.layer = 2;
        cloneObject.GetComponent<Furniture>().isMove = true;
        cloneObject.GetComponent<Furniture>().materialArray = snappedObject.GetComponent<Furniture>().materialArray;
        cloneObject.GetComponent<Furniture>().materialArray = snappedObject.GetComponent<Furniture>().materialArray;
        Furniture.isFurnitureSelected = true;
    }

    private void OnTriggerCloseContextMenu(object sender, ClickedEventArgs e)
    {
        if (!isContextMenu)//If not pointed at another context menu button
        {
            snappedObject.GetComponent<Furniture>().isRotate = false;
            DeselectFurniture();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public override void OnHover(object sender, PointerEventArgs e)
    {
        if (e.target == GetComponent<Collider>().transform)
        {
            isContextMenu = true;
            turnOn();
            controllerInput.TriggerClicked += OnSelectButton;
        }
    }

    public override void OffHover(object sender, PointerEventArgs e)
    {
        if (e.target == GetComponent<Collider>().transform)
        {
            isContextMenu = false;
            turnOff();
            controllerInput.TriggerClicked -= OnSelectButton;
        }
    }
    public override void OnSelectButton(object sender, ClickedEventArgs e)
    {
        Debug.Log(selectedAction + "context menu button pressed");
        switch (selectedAction)
        {
            case action.move:
                //TODO move furniture code
                MoveFurniture();
                Destroy(gameObject.transform.parent.gameObject);
                break;

            case action.rotate:
                RotateFurniture();
          
                break;

            case action.clone:
                //TODO clone furniture code
                CloneFurniture();
                Destroy(gameObject.transform.parent.gameObject);
                break;

            case action.del:
                DeselectFurniture();
                Destroy(snappedObject);
                Destroy(gameObject.transform.parent.gameObject);
                break;
        }

        

    }

    private void OnPadClickedRotate(object sender, ClickedEventArgs e)
    {
        snappedObject.GetComponent<Furniture>().isRotate = true;
    }

    private void OnPadUnclickedStopRotate(object sender, ClickedEventArgs e)
    {
        snappedObject.GetComponent<Furniture>().isRotate = false;
    }

    private void OnDestroy()
    {
        Debug.Log("Running OnDestroy for Context Menu Button: " + selectedAction);
        
        pointer.PointerIn -= OnHover;
        pointer.PointerOut -= OffHover;
        controllerInput.TriggerClicked -= OnTriggerCloseContextMenu;
        controllerInput.TriggerClicked -= OnSelectButton;
        controllerInput.PadClicked -= OnPadClickedRotate;
        controllerInput.PadUnclicked -= OnPadUnclickedStopRotate;
    }

}
