using UnityEngine;
using System.Collections;

public class CatalogManager : MonoBehaviour {

	private GameObject[] catalog;
	private Sprite[] catalogPreviews;
	private int catalogSize;
	private int catalogStart;
	private int catalogStop;
	private bool isActive = false;

	private UnityEngine.UI.Image[] previews;
	private UnityEngine.UI.Image[] backs;

	public Canvas catalogCanvas;
    public SteamVR_TrackedController controllerInput;



    // Use this for initialization
    void Start () {
		catalog = Resources.LoadAll<GameObject>("Prefabs");
		catalogSize = catalog.Length;
		catalogStart = 0;
		LoadAllObjectPreviews();

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

		ShowObjectPreviews();
		catalogCanvas.gameObject.SetActive(false);


        // experimental
        controllerInput.MenuButtonClicked += displayCatalog;
    }

    public virtual void displayCatalog(object sender, ClickedEventArgs e)
    {
        if (isActive)
            catOff();
        else
            catOn();

    }

    private void LoadAllObjectPreviews() {
		catalogPreviews = new Sprite[catalogSize];

		for(int i = 0; i < catalogSize; i++) {
			catalogPreviews[i] = getPreviewSprite(catalog[i]);
		}
	}

	private Sprite getPreviewSprite(GameObject prefab) {
		Texture2D texture = UnityEditor.AssetPreview.GetAssetPreview(prefab);
		if(texture == null) {
			UnityEditor.AssetPreview.GetAssetPreview(prefab);
		}

		Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
		return newSprite;
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
	}

	public void catOn() {
		Debug.Log("Catalog on");
		catalogCanvas.gameObject.SetActive(true);
		catalogCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3.0f;

		isActive = true;
	}

	public void scrollForward() {
		// check meed to scroll
		if ( !isActive) {
			Debug.LogWarning("Catalog off. Cannot Scroll");
			return;
		} else if (catalogStop == catalogSize) {
			Debug.LogWarning("End of catalog reached");
			return;
		}

		Debug.Log("Scrolling forward");

		// remove buttons from view
		toggleCatButtons(-1);


		if ( catalogStop + 6 < catalogSize) {
			catalogStart = catalogStop;
			catalogStop += 6;
		}
		else {
			catalogStart = catalogStop;
			catalogStop = catalogSize;
		}

		ShowObjectPreviews();
	}

	public void scrollBackward() {
		// check meed to scroll
		if (!isActive) {
			Debug.LogWarning("Catalog off. Cannot Scroll");
			return;
		} else if (catalogStart == 0) {
			Debug.LogWarning("Start of catalog reached");
			return;
		}

		Debug.Log("Scrolling back");

		// remove buttons from view
		toggleCatButtons(-1);

		if (catalogStart - 6 < 0) {
			catalogStop = catalogStart;
			catalogStart = 0;
		}
		else {
			
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

	public void spawnByCatalogButton(int buttonID, Vector3 location) {
		if (buttonID < 1 || buttonID > 6) {
			Debug.LogWarning("Button index out of range");
			return;
		}

		int index = (buttonID - 1) + catalogStart;

        GameObject spawn = catalog[index];
        spawn.GetComponent<Furniture>().fromCatalog = true;
		Instantiate(spawn, location, catalog[index].transform.rotation);
	}
}
