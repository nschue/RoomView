using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Furniture : MonoBehaviour {

    public SteamVR_TrackedObject controller;
    public GameObject contextMenuPrefab;
    public static bool isFurnitureSelected = false;
    public bool isSelected = false;
    public bool isRotate = false;
    public bool isMove = false;
    public bool isClone = false;
    //public bool fromCatalog = false;
    //public bool needsPlacement = false;
    //public bool placing = false;

    //[DontSaveMember] private Material highlightMaterial;

    //[DontSaveMember] private Material[] materialArray;

    //[DontSaveMember] private Material[] materialArrayWithHighlight;

    private Vector3 offset;
    private SteamVR_TrackedController controllerInput;
    private SteamVR_LaserPointer pointer;
    public bool isHover = false;
    public bool isMoving = false;
    private Quaternion targetRotation = Quaternion.identity;
    private Vector3 rotationDifference;
    private float padX;
    private int id;

	//[DontSaveMember]
	//Material thisShader;
    


    // Use this for initialization
    void Awake() {
        
        //Can't be completed on furniture in the room at start


        

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

    void Start()
    {
        contextMenuPrefab = Resources.Load("UI Prefabs/ContextMenu") as GameObject;
        id = GetComponent<ObjectCategory>().objectID;


        if (isClone)
        {
            pointer.PointerIn += OnHover;
            pointer.PointerOut += OffHover;
            controllerInput.PadClicked += OnPadClicked;
			//isClone = false;
        }
		GameObject controllerObject = GameObject.FindGameObjectWithTag("Right Controller");
        if (controller == null)
            controller = controllerObject.GetComponent<SteamVR_TrackedObject>();

        if(controllerInput == null)
        {
            controllerInput = controllerObject.GetComponent<SteamVR_TrackedController>();
            controllerInput.PadClicked += OnPadClicked;
        }
            
        if(pointer == null)
        {
            pointer = controllerObject.GetComponent<SteamVR_LaserPointer>();
            pointer.PointerIn += OnHover;
            pointer.PointerOut += OffHover;
        }
        ObjectIdentifier objectIdentifier = gameObject.GetComponent<ObjectIdentifier>();
        objectIdentifier.id = gameObject.GetComponent<ObjectCategory>().objectID.ToString();
        objectIdentifier.componentSaveMode = ObjectIdentifier.ComponentSaveMode.All;

        try {
			GetComponent<MeshRenderer>().sharedMaterial = new Material(GetComponent<MeshRenderer>().sharedMaterial);
		}
		catch {
			MeshRenderer[] renders = GetComponentsInChildren<MeshRenderer>();
			foreach(MeshRenderer renderer in renders) {
				renderer.sharedMaterial = new Material(renderer.sharedMaterial);
			}
		}
		


		UnityEditor.PrefabUtility.DisconnectPrefabInstance(this);
    }

    private void Update()
    {

        if (isRotate)
        {

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

        if (isMove & !isMoving /*& !fromCatalog*/)
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


        if (isSelected || isHover)//If hover or selected highlight
        {
            //gameObject.GetComponent<MeshRenderer>().materials = materialArrayWithHighlight;
			try {
				GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));
			}
			catch {
				MeshRenderer[] renders = GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer  renderer in renders) {
					renderer.sharedMaterial.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));
				}
			}
		}

        else {
			try
			{
				GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", new Color());
			}
			catch
			{
				MeshRenderer[] renders = GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer renderer in renders)
				{
					renderer.sharedMaterial.SetColor("_EmissionColor", new Color());
				}
			}
		}
			


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
