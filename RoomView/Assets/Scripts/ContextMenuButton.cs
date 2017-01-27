using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuButton : MenuButton {


    public enum action
    {
        move, rotate, clone, del
    }

    public Sprite onSprite;     // holds the on sprite
    public Sprite offSprite;    // holds the off sprite
    public action selectedAction;

    private bool isRotate = false;
    private static bool isContextMenu; 
    private bool isOn = false;	// button status
    private UnityEngine.UI.Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<UnityEngine.UI.Image>();
        controllerInput.TriggerClicked += CloseContextMenu;

    }

    private void Update()
    {
        if (isRotate)
            transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject.transform.rotation =
                controller.transform.rotation;
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

    private void CloseContextMenu(object sender, ClickedEventArgs e)
    {
        if (!isContextMenu)
        {
            DeselectFurniture();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void DeselectFurniture()
    {
        gameObject.transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject.GetComponent<Furniture>().isSelected = false;
    }

    private void RotateFurniture()
    {
        //Hides the context menu button
        foreach(Transform child in transform.parent)
        {
            child.gameObject.SetActive(false);
        }
        controllerInput.PadClicked += PadClicked;
    }

    public override void OnHover(object sender, PointerEventArgs e)
    {
        if (e.target == GetComponent<Collider>().transform)
        {
            isContextMenu = true;
            controllerInput.TriggerClicked -= CloseContextMenu;
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
            controllerInput.TriggerClicked += CloseContextMenu;
        }
    }
    public override void OnSelectButton(object sender, ClickedEventArgs e)
    {
        Debug.Log(selectedAction + "context menu button pressed");
        switch (selectedAction)
        {
            case action.move:
                //TODO move furniture code
                DeselectFurniture();
                Destroy(gameObject.transform.parent.gameObject);
                break;

            case action.rotate:
                //TODO rotate furniture code
                RotateFurniture();
                
                break;

            case action.clone:
                //TODO clone furniture code
                DeselectFurniture();
                Destroy(gameObject.transform.parent.gameObject);
                break;

            case action.del:
                DeselectFurniture();
                Destroy(transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject);
                Destroy(gameObject.transform.parent.gameObject);
                break;
        }

        

    }

    private void PadClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log(e.padX);
        //Check x position of touch and then rotate
        transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject.GetComponent<Furniture>().isRotate = !transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject.GetComponent<Furniture>().isRotate;
        Debug.Log("isRotate set to: " + transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject.GetComponent<Furniture>().isRotate);
        //if (e.padX>0)
        //{
        //    float rotationAngle = 15f;
        //    Transform furnitureTransform = gameObject.transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject.transform;
        //    Quaternion targetQuat = Quaternion.AngleAxis(rotationAngle, Vector3.up) * furnitureTransform.rotation;
        //    furnitureTransform.rotation = targetQuat;
        //    Debug.Log("Rotate item right");
        //}
        //else
        //{
        //    float rotationAngle = -15f;
        //    Transform furnitureTransform = gameObject.transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject.transform;
        //    Quaternion targetQuat = Quaternion.AngleAxis(rotationAngle, Vector3.up) * furnitureTransform.rotation;
        //    furnitureTransform.rotation = targetQuat;
        //    Debug.Log("Rotate item left");
        //}
    }

    private void OnDestroy()
    {
        Debug.Log("Running OnDestroy for Context Menu Button" + selectedAction);
        pointer.PointerIn -= OnHover;
        pointer.PointerOut -= OffHover;
        controllerInput.TriggerClicked -= CloseContextMenu;
        controllerInput.TriggerClicked -= OnSelectButton;
        controllerInput.PadClicked -= PadClicked;
    }

}
