using UnityEngine;
using System.Collections;

public class ObjectCategory : MonoBehaviour {

    // static variables
    public enum ROOMCODE
    {
        KITCHEN, BATHROOM, BEDROOM, LIVING, DINING, REC, OUTDOORS, NONE, ALL
    }
    public enum OBJECTTYPE
    {
        APPLIANCE, COMFORT, DECORATION, ELECTRONIC, KIDS, LIGHTING, PLUMBING, SURFACES, MISC, NONE, ALL
    }
    //public static int IDASSIGN = 1;

    // object variables
    public ROOMCODE roomType = ROOMCODE.KITCHEN;
	public OBJECTTYPE objectType = OBJECTTYPE.APPLIANCE;
	public int objectID;

	public ROOMCODE getRoomType() { return roomType; }	

}
