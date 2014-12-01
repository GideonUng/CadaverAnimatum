using UnityEngine;
using System.Collections;

using System.IO;


public class MainMenuGUI : MonoBehaviour
{
	public Rect rect;
	public SaveDataReference saveDataRef;

	string[] saveFiles;

	void Start()
	{
		rect = new Rect(10, 10, 170, 300);
		saveDataRef = GameObject.Find("Save Controller").GetComponent<SaveDataReference>();

		saveFiles = Directory.GetFiles(Application.dataPath);

		foreach (string savefile in saveFiles)
		{
			if(savefile.Contains("SaveData0.save"))
			{
				SaveLoad.Load();
				break;
			}
		}

	}

	void OnGUI()
	{
		if (!Application.isLoadingLevel)
		{
			// Make a background box
			GUI.Box(rect, "");

			GUILayout.BeginArea(rect);

			GUILayout.Label("Main Menu");

			foreach (string savefile in saveFiles)
			{
				if(savefile.Contains("SaveData0.save"))
				{
					if(GUILayout.Button("Continue"))
					{
						print(saveDataRef.saveData.data.currentLevel);
						Application.LoadLevel(saveDataRef.saveData.data.currentLevel);
					}
					break;
				}
			}

			if (GUILayout.Button("Start new game"))
			{
				Application.LoadLevel("Intro");
				saveDataRef.saveData.data = new Data();
			}

			if (GUILayout.Button("Credits"))
			{
				Application.LoadLevel("Credits");
			}

			if (GUILayout.Button("Quit"))
			{
				Application.Quit();
			}

			GUILayout.EndArea();
		}
	}
}