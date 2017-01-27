using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Furniture : MonoBehaviour {

    public SteamVR_TrackedObject controller;
    public Material highlightMaterial;
    public GameObject contextMenuPrefab;
    public bool isSelected = false;


    private SteamVR_TrackedController controllerInput;
    private SteamVR_LaserPointer pointer;
    private Material[] materialArray;
    private Material[] newMaterialArray;
    private bool isHover = false;
    
    // Use this for initialization
    void Awake () {
	    if(controller !=null )
        {
            try
            {
                pointer = controller.GetComponent<SteamVR_LaserPointer>();
                controllerInput = controller.GetComponent<SteamVR_TrackedController>();
            }
            catch
            {
                Debug.LogError(this.name +": Could not find LaserPointer");
            }
        }
	}

    private void OnEnable()
    {
        pointer.PointerIn += OnHover;
        pointer.PointerOut += OffHover;
    }

     void Start()
    {
        List<Material> materials = gameObject.GetComponent<MeshRenderer>().materials.Cast<Material>().ToList();
        List<Material> newMaterials = gameObject.GetComponent<MeshRenderer>().materials.Cast<Material>().ToList();
        newMaterials.Add(highlightMaterial);
        materialArray = materials.ToArray();
        newMaterialArray = newMaterials.ToArray();
    }

    private void Update()
    {
        if(isSelected || isHover)
            gameObject.GetComponent<MeshRenderer>().materials = newMaterialArray;
        else
            gameObject.GetComponent<MeshRenderer>().materials = materialArray;

    }

    void OnHover(object sender, PointerEventArgs e)
    {
        if(e.target == GetComponent<Collider>().transform)
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
    }

    private void OnDestroy()
    {
        pointer.PointerIn -= OnHover;
        pointer.PointerOut -= OffHover;
        controllerInput.TriggerClicked -= OnSelectButton;
    }
}
