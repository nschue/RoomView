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

    private static bool isContextMenu; 
    private bool isOn = false;	// button status
    private UnityEngine.UI.Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<UnityEngine.UI.Image>();
        controllerInput.TriggerClicked += CloseContextMenu;

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
        if(e.target == GetComponent<Collider>().transform)
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
            case action.del:
                Destroy(transform.parent.gameObject.GetComponent<UI_Follower>().snappedObject);
                break;
        }

        Destroy(gameObject.transform.parent.gameObject);

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
            Destroy(gameObject.transform.parent.gameObject);
        }
    }


    private void OnDestroy()
    {
        Debug.Log("Running OnDestroy");
        pointer.PointerIn -= OnHover;
        pointer.PointerOut -= OffHover;
        controllerInput.TriggerClicked -= CloseContextMenu;
        controllerInput.TriggerClicked -= OnSelectButton;
    }
}
