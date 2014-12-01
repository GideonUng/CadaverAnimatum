using UnityEngine;
using System.Collections;

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;

public class SaveController : MonoBehaviour
{
	bool showGUI;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}

// === This is the class that will be accessed from scripts ===
public class SaveLoad
{
	public static string currentFilePath = Application.dataPath + "/"; // Edit this for different save files

	// Call this to write data
	public static void Save(bool newSavePoint) // Overloaded
	{
		Save(currentFilePath, newSavePoint);
	}

	public static void Save(string filePath, bool newSavePoint)
	{
		SaveData data = GameObject.Find("Save Controller").GetComponent<SaveDataReference>().saveData;


		Stream stream;

		if (newSavePoint)
		{
			int saveFilesCount = 0;

			foreach (string fileName in Directory.GetFiles(currentFilePath))
			{
				if (fileName.Contains("SaveData") && !fileName.Contains("meta"))
				{
					saveFilesCount++;
				}
			}

			stream = File.Open(currentFilePath + "SaveData" + saveFilesCount + ".save", FileMode.Create);
		}
		else
		{
			stream = File.Open(filePath + "SaveData0.save", FileMode.Create);
		}

		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new VersionDeserializationBinder();
		bformatter.Serialize(stream, data);
		stream.Close();
	}

	// Call this to load from a file into "data"
	public static void Load()
	{
		Load(currentFilePath + "SaveData0.save");
	} // Overloaded
	public static void Load(string filePath)
	{
		SaveData data = new SaveData();

		Stream stream = File.Open(filePath, FileMode.Open);
		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new VersionDeserializationBinder();
		data = (SaveData)bformatter.Deserialize(stream);
		stream.Close();

		GameObject.Find("Save Controller").GetComponent<SaveDataReference>().saveData = data;
	}
}

// === This is required to guarantee a fixed serialization assembly name, which Unity likes to randomize on each compile
// Do not change this
public sealed class VersionDeserializationBinder : SerializationBinder
{
	public override Type BindToType(string assemblyName, string typeName)
	{
		if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
		{
			Type typeToDeserialize = null;

			assemblyName = Assembly.GetExecutingAssembly().FullName;

			// The following line of code returns the type.
			typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

			return typeToDeserialize;
		}

		return null;
	}
}