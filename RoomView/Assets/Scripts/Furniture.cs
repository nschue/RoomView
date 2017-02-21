﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Furniture : MonoBehaviour {

    public SteamVR_TrackedObject controller;
    public Material highlightMaterial;
    public GameObject contextMenuPrefab;
    public static bool isFurnitureSelected = false;
    public bool isSelected = false;
    public bool isRotate = false;
    public bool isMove = false;
    public bool isClone = false;
    public Material[] materialArray;
    public Material[] materialArrayWithHighlight;

    public bool fromCatalog = false;
    public bool needsPlacement = false;
    public bool placing = false;


    private Vector3 offset;
    private SteamVR_TrackedController controllerInput;
    private SteamVR_LaserPointer pointer;
    private bool isHover = false;
    private bool isMoving = false;
    private Quaternion targetRotation = Quaternion.identity;
    private Vector3 rotationDifference;
    private float padX;
    private int id;


    // Use this for initialization
    void Awake() {
        
        //Can't be completed on furniture in the room at start

        //controller is empty do this
        /*GameObject controllerObject = GameObject.FindGameObjectWithTag("Right Controller") as GameObject;
        //controller = controllerObject.GetComponent<SteamVR_TrackedObject>();*/
        

        if (controller != null)
        {
            try
            {
                pointer = controller.GetComponent<SteamVR_LaserPointer>();
                controllerInput = controller.GetComponent<SteamVR_TrackedController>();
            }
            catch
            {
                Debug.LogError(this.name + ": Could not find LaserPointer");
            }
        }
    }

    private void OnEnable()
    {
        if (fromCatalog)
        {
            isMove = false;
            needsPlacement = true;
            gameObject.layer = 2;
            pointer = GameObject.Find("Controller (right)").GetComponent<SteamVR_LaserPointer>();
            controllerInput = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();

        }
        else
        {
            pointer.PointerIn += OnHover;
            pointer.PointerOut += OffHover;
            controllerInput.PadClicked += OnPadClicked;
        }

    }

    void Start()
    {
        id = GetComponent<ObjectCategory>().objectID;
        
        if (!isClone || fromCatalog) //If it is a clone, material arrays are being set be equal to the cloned object to prevent object from always being highlighted
        {
            List<Material> materials = gameObject.GetComponent<MeshRenderer>().materials.Cast<Material>().ToList();
            List<Material> newMaterials = gameObject.GetComponent<MeshRenderer>().materials.Cast<Material>().ToList();
            newMaterials.Add(highlightMaterial);
            materialArray = materials.ToArray();
            materialArrayWithHighlight = newMaterials.ToArray();
        }

        if(controller == null)
            controller = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedObject>();

        if(controllerInput == null)
            controllerInput = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();

        if(pointer == null)
            pointer = GameObject.Find("Controller (right)").GetComponent<SteamVR_LaserPointer>();

    }

    private void Update()
    {

        if (isRotate)
        {
            //targetRotation = controller.transform.rotation;
            //targetRotation.eulerAngles = new Vector3(transform.eulerAngles.x, targetRotation.eulerAngles.y + rotationDifference.y, transform.eulerAngles.z);
            //transform.rotation = targetRotation;
            float rotationAngle = 60f * Time.deltaTime;
            if (padX>0)
            {
                Quaternion targetQuat = Quaternion.AngleAxis(rotationAngle, Vector3.up) * transform.rotation;
                transform.rotation = targetQuat;
                //Debug.Log("Rotate item right");
            }
            else
            {
                Quaternion targetQuat = Quaternion.AngleAxis(-rotationAngle, Vector3.up) * transform.rotation;
                transform.rotation = targetQuat;
                //Debug.Log("Rotate item left");
            }
        }

        if (isMove & !isMoving & !fromCatalog)
        {
            isMoving = true;
            controllerInput.TriggerClicked -= OnSelectButton;
            controllerInput.TriggerClicked += CompleteMove;

            CatalogManager manager = GameObject.Find("PrefabCatalog").GetComponent<CatalogManager>();
            offset = manager.getObjectByID(id).transform.position;
        }        
        
        else if (isMove)//Conext menu button sets isMove
        {
            Ray raycast = new Ray(controller.transform.position, controller.transform.forward); //
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);
            transform.position = hit.point + offset;
        }

        if(needsPlacement && !placing){
            placing = true;
            controllerInput.TriggerClicked += CompleteMove;
            offset = GameObject.Find("PrefabCatalog").GetComponent<CatalogManager>().getObjectByID(id).transform.position;
        }
        else if (needsPlacement)
        {
            Ray raycast = new Ray(controller.transform.position, controller.transform.forward); //
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);
            transform.position = hit.point + offset;
        }


        if (isSelected || isHover)//If hover or selected highlight
            gameObject.GetComponent<MeshRenderer>().materials = materialArrayWithHighlight;
        else
            gameObject.GetComponent<MeshRenderer>().materials = materialArray;

    }

    void CompleteMove(object sender, ClickedEventArgs e)
    {
        Debug.Log("Furniture.CompleteMove: BEGIN");
        isSelected = false;
        isHover = false;
        isFurnitureSelected = false;
        isMove = false;
        gameObject.layer = 0;
        controllerInput.TriggerClicked -= CompleteMove;
        isMoving = false;
        Debug.Log("Furniture.CompleteMove: END");

        if (needsPlacement == true)
        {
            needsPlacement = false;
            pointer.PointerIn += OnHover;
            pointer.PointerOut += OffHover;
            controllerInput.PadClicked += OnPadClicked;
            fromCatalog = false;
        }
    }

    void OnHover(object sender, PointerEventArgs e)
    {
        if((e.target == GetComponent<Collider>().transform) && !isSelected && !isFurnitureSelected)
        {
            controllerInput.TriggerClicked += OnSelectButton;
            isHover = true;
        }
    }

    void OffHover(object sender, PointerEventArgs e)
    {
        if(e.target == GetComponent<Collider>().transform)
        {
            isHover = false;
            controllerInput.TriggerClicked -= OnSelectButton;
        }
    }

    public virtual void OnSelectButton(object sender, ClickedEventArgs e)
    {
        GameObject contextMenu = Instantiate(contextMenuPrefab)as GameObject;
        contextMenu.GetComponent<UI_Follower>().mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        contextMenu.GetComponent<UI_Follower>().setSnappedObject(gameObject);
        isSelected = true;
        isFurnitureSelected = true;
        
    }

    private void OnPadClicked(object sender, ClickedEventArgs e)
    {
        padX = e.padX;
    }

    private void OnDestroy()
    {
        pointer.PointerIn -= OnHover;
        pointer.PointerOut -= OffHover;
        controllerInput.TriggerClicked -= OnSelectButton;
        controllerInput.PadClicked -= OnPadClicked;
    }
}
