using UnityEngine;
using System.Collections;

public class saveCurrentLevel : MonoBehaviour 
{
	public int level;
	public SaveData saveData;

	void Start()
	{
		saveData = GameObject.Find("Save Controller").GetComponent<SaveDataReference>().saveData;

		if(saveData.data.currentLevel != Application.loadedLevel)
		{
			saveData.data.currentLevel = level;
			SaveLoad.Save(false);
		}
	}
}
