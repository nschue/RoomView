using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoad_Custom : MonoBehaviour
{
	

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public bool SaveData(string FileName)
	{
		StreamWriter writer;
		GameObject[] allFurniture;

		File.Delete(FileName);

		try
		{
			writer = new StreamWriter(new FileStream(FileName, FileMode.OpenOrCreate));
		}
		catch
		{
			Debug.LogError("Cannot access File");
			return false;
		}

		try
		{
			allFurniture = GameObject.FindGameObjectsWithTag("Furniture");
			Debug.Log("There are this many objects: " + allFurniture.Length);
			writer.Write(allFurniture.Length + "\n");

			foreach (GameObject furniture in allFurniture)
			{

				


				string toFile = furniture.name + "," +
				furniture.name.Replace("(Clone)" , "") + "," +
				furniture.transform.position.x.ToString("0.00") + "," +
				furniture.transform.position.y.ToString("0.00") + "," +
				furniture.transform.position.z.ToString("0.00") + "," +

				furniture.transform.rotation.eulerAngles.x.ToString("0.00") + "," +
				furniture.transform.rotation.eulerAngles.y.ToString("0.00") + "," +
				furniture.transform.rotation.eulerAngles.z.ToString("0.00") + "\n";

				

				writer.Write(toFile);
			}
		}
		catch
		{
			Debug.LogError("Cannot write to file");
			writer.Close();
			return false;
		}

		writer.Close();
		return true;
	}

	public bool LoadData(string FileName)
	{

		GameObject[] allFurniture = GameObject.FindGameObjectsWithTag("Furniture");

		if(allFurniture.Length > 0) {
			foreach(GameObject furn in allFurniture) {
				Destroy(furn);
			}
		}

		StreamReader reader;
		//GameObject[] allFurniture;

		try
		{
			reader = new StreamReader(new FileStream(FileName, FileMode.Open));
			//new BinaryWriter(new FileStream(FileName, FileMode.OpenOrCreate));
		}
		catch
		{
			Debug.LogError("Cannot access File");
			return false;
		}

		try
		{
			int size = int.Parse(reader.ReadLine());
			Debug.Log("There are this many objects to load: " + size);
			
			string temp;
			while ((temp = reader.ReadLine()) != null)
			{
				CreateObject(temp);
			}
		}
		catch
		{
			Debug.LogError("Cannot read from file");
			reader.Close();
			return false;
		}

		reader.Close();
		return true;
	}

	void CreateObject(string fromFile) {
		string[] tokens = fromFile.Split(',');
		Debug.Log("string size: " + tokens.Length);

		GameObject newFurniture = Instantiate(Resources.Load<GameObject>("Prefabs/" + tokens[1]));
		newFurniture.name = tokens[0];
		newFurniture.transform.position = new Vector3(float.Parse(tokens[2]), float.Parse(tokens[3]), float.Parse(tokens[4]));
		newFurniture.transform.eulerAngles = new Vector3(float.Parse(tokens[5]), float.Parse(tokens[6]), float.Parse(tokens[7]));

		

	}

}