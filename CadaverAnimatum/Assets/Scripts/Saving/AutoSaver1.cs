using UnityEngine;
using System.Collections;

public class AutoSaver1 : MonoBehaviour
{
	public SaveData saveData;
	public int savePointNumber;

	void Start()
	{
		saveData = GameObject.Find("Save Controller").GetComponent<SaveDataReference>().saveData;
	}

	void OnTriggerEnter()
	{
		if (saveData.data.lastSavePoint < savePointNumber)
		{
			saveData.data.lastSavePoint = savePointNumber;
			SaveLoad.Save(false);
		}
	}
}
