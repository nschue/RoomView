using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatalogManager : MonoBehaviour {

	private GameObject[] catalog;
	private Sprite[] catalogPreviews;
	private int catalogSize;
	private int catalogStart;
	private int catalogStop;
	
    //
    private int[] indexInFilteredCatalog;
    private int filteredCatalogSize;
    private int filtCatalogStart;
    private int filtCatalogStop;
    private bool filterActive = false;
    private UnityEngine.UI.Text titleText;
    private ObjectCategory.ROOMCODE filtRoomCode = ObjectCategory.ROOMCODE.ALL;
    private ObjectCategory.OBJECTTYPE filtTypeCode = ObjectCategory.OBJECTTYPE.ALL;
    //
    private UnityEngine.UI.Image[] previews;
	private UnityEngine.UI.Image[] backs;

	public Canvas catalogCanvas;
    public SteamVR_TrackedController controllerInput;

    [HideInInspector]
    public bool isActive = false;

    // Use this for initialization
    void Start () {
        titleText = GameObject.Find("Title Text").GetComponent<UnityEngine.UI.Text>();
        titleText.text = "Catalog > Room: All    Category: All";
        catalog = Resources.LoadAll<GameObject>("Prefabs");
		catalogSize = catalog.Length;
        //
        if (catalogSize > 49)
            catalogSize = 49;

        //
        indexInFilteredCatalog = new int[catalogSize];
        catalogPreviews = new Sprite[catalogSize];
        catalogStart = 0;
		
		if (catalogSize > 6)
			catalogStop = 6;
		else
			catalogStop = catalogSize;

		previews = new UnityEngine.UI.Image[6];
		backs = new UnityEngine.UI.Image[6];
		previews[0] = GameObject.Find("Preview1").GetComponent<UnityEngine.UI.Image>();
		previews[1] = GameObject.Find("Preview2").GetComponent<UnityEngine.UI.Image>();
		previews[2] = GameObject.Find("Preview3").GetComponent<UnityEngine.UI.Image>();
		previews[3] = GameObject.Find("Preview4").GetComponent<UnityEngine.UI.Image>();
		previews[4] = GameObject.Find("Preview5").GetComponent<UnityEngine.UI.Image>();
		previews[5] = GameObject.Find("Preview6").GetComponent<UnityEngine.UI.Image>();
		backs[0] = GameObject.Find("Back1").GetComponent<UnityEngine.UI.Image>();
		backs[1] = GameObject.Find("Back2").GetComponent<UnityEngine.UI.Image>();
		backs[2] = GameObject.Find("Back3").GetComponent<UnityEngine.UI.Image>();
		backs[3] = GameObject.Find("Back4").GetComponent<UnityEngine.UI.Image>();
		backs[4] = GameObject.Find("Back5").GetComponent<UnityEngine.UI.Image>();
		backs[5] = GameObject.Find("Back6").GetComponent<UnityEngine.UI.Image>();

        StartCoroutine(LoadAllObjectPreviewsCo()); //LoadAllObjectPreviews();
        //ShowObjectPreviews();
        catalogCanvas.gameObject.SetActive(false);
        Debug.Log("input");
        controllerInput.MenuButtonClicked += displayCatalog;
    }

    IEnumerator LoadAllObjectPreviewsCo()
    {
        //catalogPreviews = new Sprite[catalogSize];
        Texture2D texture;
        Sprite newSprite;

        for (int i = 0; i < catalogSize; i++)
        {
            UnityEditor.AssetPreview.GetAssetPreview(catalog[i]);
        }

        Debug.Log("Waiting  to load "+catalogSize + " objects");
        yield return new WaitForSeconds(2.0f);
        
        for (int i = 0; i < catalogSize; i++)
        {

            texture = null;
            texture = UnityEditor.AssetPreview.GetAssetPreview(catalog[i]);
            if(texture == null)
            {
                Debug.Log("waiting");
                yield return new WaitForSeconds(0.1f);
                i--;
                continue;
            }
                
            //Debug.Log("loading " + i);
            newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            //Debug.Log("loaded " + i);
            catalogPreviews[i] = newSprite;
        }

        ShowObjectPreviews();
        yield return null;
    }

    public virtual void displayCatalog(object sender, ClickedEventArgs e)
    {
        if (isActive)
        {
            catOff();
        }

        else
        {
            catOn();
        }
    }

    //Move all GameObjects to IgnoreRaycast layer
    private void raycastIgnoreOtherObjects()
    {
        GameObject[] sceneObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject gObject in sceneObjects)
        {
            GameObject parentObject;
            Transform parentTransform = gObject.transform;
            while (parentTransform.parent != null)
            {
                parentTransform = parentTransform.parent;
            }
            parentObject = parentTransform.gameObject;
            if (parentObject != catalogCanvas.gameObject)
            {
                gObject.layer = 2;
            }
        }
    }

    //Move all GameObjects back to default layer
    private void raycastHitOtherObjects()
    {
        GameObject[] sceneObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject gObject in sceneObjects)
        {
            GameObject parentObject;
            Transform parentTransform = gObject.transform;
            while(parentTransform.parent != null)
            {
                parentTransform = parentTransform.parent;
            }
            parentObject = parentTransform.gameObject;
            if (parentObject != catalogCanvas)
            {
                if(parentObject.GetComponent<Furniture>() != null && parentObject.GetComponent<Furniture>().isMove)
                {
                    gObject.layer = 2;
                }
                else
                    gObject.layer = 0;
            }
        }
    }

	private void ShowObjectPreviews() {
		int previewsToShow = catalogStop % 6;
		toggleCatButtons(previewsToShow);

		for(int i = 0, showing = catalogStart ; showing < catalogStop; i++, showing++) {
			previews[i].sprite = catalogPreviews[showing];
		}
	}

	private void toggleCatButtons(int previewsToShow) {
		switch (previewsToShow) {
			case -1:
				backs[0].gameObject.SetActive(false);
				backs[1].gameObject.SetActive(false);
				backs[2].gameObject.SetActive(false);
				backs[3].gameObject.SetActive(false);
				backs[4].gameObject.SetActive(false);
				backs[5].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(false);
				previews[1].gameObject.SetActive(false);
				previews[2].gameObject.SetActive(false);
				previews[3].gameObject.SetActive(false);
				previews[4].gameObject.SetActive(false);
				previews[5].gameObject.SetActive(false);
				break;

			case 1:
				backs[0].gameObject.SetActive(true);
				backs[1].gameObject.SetActive(false);
				backs[2].gameObject.SetActive(false);
				backs[3].gameObject.SetActive(false);
				backs[4].gameObject.SetActive(false);
				backs[5].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(true);
				previews[1].gameObject.SetActive(false);
				previews[2].gameObject.SetActive(false);
				previews[3].gameObject.SetActive(false);
				previews[4].gameObject.SetActive(false);
				previews[5].gameObject.SetActive(false);
				break;

			case 2:
				backs[0].gameObject.SetActive(true);
				backs[1].gameObject.SetActive(true);
				backs[2].gameObject.SetActive(false);
				backs[3].gameObject.SetActive(false);
				backs[4].gameObject.SetActive(false);
				backs[5].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(true);
				previews[1].gameObject.SetActive(true);
				previews[2].gameObject.SetActive(false);
				previews[3].gameObject.SetActive(false);
				previews[4].gameObject.SetActive(false);
				previews[5].gameObject.SetActive(false);
				break;

			case 3:
				backs[0].gameObject.SetActive(true);
				backs[1].gameObject.SetActive(true);
				backs[2].gameObject.SetActive(true);
				backs[3].gameObject.SetActive(false);
				backs[4].gameObject.SetActive(false);
				backs[5].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(true);
				previews[1].gameObject.SetActive(true);
				previews[2].gameObject.SetActive(true);
				previews[3].gameObject.SetActive(false);
				previews[4].gameObject.SetActive(false);
				previews[5].gameObject.SetActive(false);
				break;

			case 4:
				backs[0].gameObject.SetActive(true);
				backs[1].gameObject.SetActive(true);
				backs[2].gameObject.SetActive(true);
				backs[3].gameObject.SetActive(true);
				backs[4].gameObject.SetActive(false);
				backs[5].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(true);
				previews[1].gameObject.SetActive(true);
				previews[2].gameObject.SetActive(true);
				previews[3].gameObject.SetActive(true);
				previews[4].gameObject.SetActive(false);
				previews[5].gameObject.SetActive(false);
				break;

			case 5:
				backs[0].gameObject.SetActive(true);
				backs[1].gameObject.SetActive(true);
				backs[2].gameObject.SetActive(true);
				backs[3].gameObject.SetActive(true);
				backs[4].gameObject.SetActive(true);
				backs[5].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(true);
				previews[1].gameObject.SetActive(true);
				previews[2].gameObject.SetActive(true);
				previews[3].gameObject.SetActive(true);
				previews[4].gameObject.SetActive(true);
				previews[5].gameObject.SetActive(false);
				break;

			case 0:
				backs[0].gameObject.SetActive(true);
				backs[1].gameObject.SetActive(true);
				backs[2].gameObject.SetActive(true);
				backs[3].gameObject.SetActive(true);
				backs[4].gameObject.SetActive(true);
				backs[5].gameObject.SetActive(true);
				previews[0].gameObject.SetActive(true);
				previews[1].gameObject.SetActive(true);
				previews[2].gameObject.SetActive(true);
				previews[3].gameObject.SetActive(true);
				previews[4].gameObject.SetActive(true);
				previews[5].gameObject.SetActive(true);
				break;

			default:
				backs[0].gameObject.SetActive(false);
				backs[1].gameObject.SetActive(false);
				backs[2].gameObject.SetActive(false);
				backs[3].gameObject.SetActive(false);
				backs[4].gameObject.SetActive(false);
				backs[5].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(false);
				previews[0].gameObject.SetActive(false);
				break;
		}
	}

	public void catOff() {
		Debug.Log("Catalog off");
		catalogCanvas.transform.position = new Vector3(0.0f, -100.0f, 0.0f);
		catalogCanvas.gameObject.SetActive(false);

		catalogStart = 0;
		if (catalogSize > 6)
			catalogStop = 6;
		else
			catalogStop = catalogSize;
		ShowObjectPreviews();

		isActive = false;
        raycastHitOtherObjects();
    }

	public void catOn() {
		Debug.Log("Catalog on");
		catalogCanvas.gameObject.SetActive(true);
		catalogCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3.0f;

		isActive = true;
        raycastIgnoreOtherObjects();
    }

	public void scrollForward() {
        // check need to scroll
        if (!isActive)
        {
            Debug.LogWarning("Catalog off. Cannot Scroll");
            return;
        }
        else if (filterActive)
        {
            filtScrollForward();
            return;
        }
        else if (catalogStop == catalogSize)
        {
            Debug.LogWarning("End of catalog reached");
            return;
        }

        Debug.Log("Scrolling forward");

        // remove buttons from view
        toggleCatButtons(-1);


        if (catalogStop + 6 < catalogSize)
        {
            catalogStart = catalogStop;
            catalogStop += 6;
        }
        else
        {
            catalogStart = catalogStop;
            catalogStop = catalogSize;
        }

        ShowObjectPreviews();
    }

	public void scrollBackward() {
        // check need to scroll
        if (!isActive)
        {
            Debug.LogWarning("Catalog off. Cannot Scroll");
            return;
        }
        else if (filterActive)
        {
            filtScrollBackward();
            return;
        }
        else if (catalogStart == 0)
        {
            Debug.LogWarning("Start of catalog reached");
            return;
        }

        Debug.Log("Scrolling back");

        // remove buttons from view
        toggleCatButtons(-1);

        if (catalogStart - 6 < 0)
        {
            catalogStop = catalogStart;
            catalogStart = 0;
        }
        else
        {

            catalogStop = catalogStart;
            catalogStart = catalogStart - 6;
        }

        ShowObjectPreviews();
    }

	public GameObject getGameObjectAtIndex(int indexInCatalog) {
		if(indexInCatalog >= catalogSize) {
			Debug.LogWarning("Index is larger than catalog size. Returning null");
			return null;
		}
		return catalog[indexInCatalog];
	}

	public void spawnRandom (Vector3 location) {
		int index = Random.Range(0, catalogSize);
		Instantiate(catalog[index], location, catalog[index].transform.rotation);
	}

	public void spawnObjectAtIndex (int indexInCatalog, Vector3 location) {
		if (indexInCatalog < 1 || indexInCatalog >= catalogSize) {
			Debug.LogWarning("Button index out of range");
			return;
		}

		Instantiate(catalog[indexInCatalog], location, catalog[indexInCatalog].transform.rotation);
	}

	public void spawnObjectByID (int objectID, Vector3 location) {
		for(int i = 0; i < catalogSize; i++) {
			if(catalog[i].GetComponent<ObjectCategory>().objectID == objectID) {
				Instantiate(catalog[i], location, catalog[i].transform.rotation);
				return;
			}
		}
		Debug.LogWarning("Object ID not found");
	}

    public GameObject getObjectByID(int objectID)
    {
        for (int i = 0; i < catalogSize; i++)
        {
            if (catalog[i].GetComponent<ObjectCategory>().objectID == objectID)
            {
                return catalog[i];
            }
        }
        
        Debug.LogWarning("Object ID not found");
        return null;
    }

    public void spawnByCatalogButton(int buttonID, Vector3 location) {
		if (buttonID < 1 || buttonID > 6) {
			Debug.LogWarning("Button index out of range");
			return;
		}

        Debug.Log("ButtonID: " + buttonID);

        int index;
        if (!filterActive)
        {
            index = (buttonID - 1) + catalogStart;
            GameObject furniture = Instantiate(catalog[index], location, catalog[index].transform.rotation);
            furniture.gameObject.layer = 2;
            furniture.GetComponent<Furniture>().isMove = true;
        }
        else
        {
            index = (buttonID - 1) + filtCatalogStart;
            GameObject furniture = Instantiate(catalog[indexInFilteredCatalog[index]], location, catalog[index].transform.rotation);
            furniture.gameObject.layer = 2;
            furniture.GetComponent<Furniture>().isMove = true;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // code for filters
    public ObjectCategory.ROOMCODE FiltRoomCode
    {
        get
        {
            return filtRoomCode;
        }

        set
        {
            filtRoomCode = value;
            print("Room Filter: " + filtRoomCode);
            getFilteredObjects();
            //print("Room Filter: " + filtRoomCode);
            titleText.text = "Catalog > Room: " + filtRoomCode + "    Category: " + filtTypeCode;
        }
    }

    public ObjectCategory.OBJECTTYPE FiltTypeCode
    {
        get
        {
            return filtTypeCode;
        }

        set
        {
            filtTypeCode = value;
            //print("Type Filter: " + filtTypeCode);
            getFilteredObjects();
            titleText.text = "Catalog > Room: " + filtRoomCode + "    Category: " + filtTypeCode;
        }
    }

    private void getFilteredObjects()
    {
        filteredCatalogSize = 0;
        filtCatalogStart = 0;
        filtCatalogStop = 0;

        if (filtRoomCode == ObjectCategory.ROOMCODE.ALL && filtTypeCode == ObjectCategory.OBJECTTYPE.ALL)
        {
            filterActive = false;
            catalogStart = 0;
            if (catalogSize > 6)
                catalogStop = 6;
            else
                catalogStop = catalogSize;

            ShowObjectPreviews();
            return;
        }
        else if (!(filtRoomCode == ObjectCategory.ROOMCODE.ALL) && !(filtTypeCode == ObjectCategory.OBJECTTYPE.ALL))
        {
            for (int i = 0; i < catalogSize; i++)
            {
                if (catalog[i].GetComponent<ObjectCategory>().roomType == filtRoomCode && catalog[i].GetComponent<ObjectCategory>().objectType == filtTypeCode)
                {
                    indexInFilteredCatalog[filteredCatalogSize] = i;
                    filteredCatalogSize++;
                }
            }
        }
        else if (!(filtRoomCode == ObjectCategory.ROOMCODE.ALL) && (filtTypeCode == ObjectCategory.OBJECTTYPE.ALL))
        {
            for (int i = 0; i < catalogSize; i++)
            {
                //Debug.Log(catalog[i].name);
                if (catalog[i].GetComponent<ObjectCategory>().roomType == filtRoomCode)
                {
                    indexInFilteredCatalog[filteredCatalogSize] = i;
                    filteredCatalogSize++;
                }
            }
        }
        else
        {
            for (int i = 0; i < catalogSize; i++)
            {
                if (catalog[i].GetComponent<ObjectCategory>().objectType == filtTypeCode)
                {
                    indexInFilteredCatalog[filteredCatalogSize] = i;
                    filteredCatalogSize++;
                }
            }
        }

        filterActive = true;

        filtCatalogStart = 0;
        if (filteredCatalogSize > 6)
            filtCatalogStop = 6;
        else
            filtCatalogStop = filteredCatalogSize;

        ShowFilteredPreviews();
    }

    private void ShowFilteredPreviews()
    {
        int previewsToShow;

        previewsToShow = filtCatalogStop % 6;
        if (filteredCatalogSize == 0)
        {
            toggleCatButtons(-1);
            return;
        }
        else
            toggleCatButtons(previewsToShow);

        for (int i = 0, showing = filtCatalogStart; showing < filtCatalogStop; i++, showing++)
        {
            previews[i].sprite = catalogPreviews[indexInFilteredCatalog[showing]];
        }
    }

    public void filtScrollForward()
    {
        // check meed to scroll
        if (!isActive)
        {
            Debug.LogWarning("Catalog off. Cannot Scroll");
            return;
        }
        else if (filtCatalogStop == filteredCatalogSize)
        {
            Debug.LogWarning("End of catalog reached");
            return;
        }

        Debug.Log("Scrolling forward");

        // remove buttons from view
        toggleCatButtons(-1);


        if (filtCatalogStop + 6 < filteredCatalogSize)
        {
            filtCatalogStart = filtCatalogStop;
            catalogStop += 6;
        }
        else
        {
            filtCatalogStart = filtCatalogStop;
            filtCatalogStop = filteredCatalogSize;
        }

        ShowFilteredPreviews();
    }

    public void filtScrollBackward()
    {
        // check meed to scroll
        if (!isActive)
        {
            Debug.LogWarning("Catalog off. Cannot Scroll");
            return;
        }
        else if (filtCatalogStart == 0)
        {
            Debug.LogWarning("Start of catalog reached");
            return;
        }

        Debug.Log("Scrolling back");

        // remove buttons from view
        toggleCatButtons(-1);

        if (filtCatalogStart - 6 < 0)
        {
            filtCatalogStop = filtCatalogStart;
            filtCatalogStart = 0;
        }
        else
        {

            filtCatalogStop = filtCatalogStart;
            filtCatalogStart = filtCatalogStart - 6;
        }

        ShowFilteredPreviews();
    }
}
