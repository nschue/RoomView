using UnityEngine;
using System.Collections;

public class UI_Follower : MonoBehaviour {

	public GameObject mainCamera;			// camera gameobject (for use in tracking)
	public GameObject snappedObject;		// gameObject snapped to (for use in tracking)

	private Vector3 offset;					// used to set the position between snappedObject and mainCamera

	// Use this for initialization
	void Start () {

		// set menu midway between both objects (50% between)
		offset = Vector3.Lerp(mainCamera.transform.position, snappedObject.transform.position, 0.5f);
		transform.position = offset;
	}
	
	// Update is called once per frame
	void Update () {

		// set menu midway between both objects (50% between)
		offset = Vector3.Lerp(mainCamera.transform.position, snappedObject.transform.position, 0.5f);
		transform.position = offset;

		// set canvas to face camera
		transform.LookAt(mainCamera.transform);
	}
	
	// sets the snappedObject
	public void setSnappedObject(GameObject newGO) {
        
		snappedObject = newGO;
		Debug.LogWarning("Object changed");
	}
}
