using UnityEngine;
using System.Collections;

public class CatalogCanvasFollower : MonoBehaviour {

	public GameObject cam;
    
    void Update () {
		// not exactly sure why this works but it flips look at
		transform.LookAt(2 * transform.position - cam.transform.position);
	}
}
