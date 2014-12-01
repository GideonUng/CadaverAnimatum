using UnityEngine;
using System.Collections;

public class EnableQuest : MonoBehaviour 
{
	public GameObject[] quests;

	private SaveDataReference saveDataRef;

	void Start()
	{
		saveDataRef = GameObject.Find("Save Controller").GetComponent<SaveDataReference>();
	}

	public void enabelQuest(int questNumber)
	{
		saveDataRef = GameObject.Find("Save Controller").GetComponent<SaveDataReference>();

		if(questNumber != 0)
		{
			foreach (GameObject quest in quests)
			{
				quest.SetActive(false);
			}

			quests[questNumber - 1].SetActive(true);
			saveDataRef.saveData.data.quest = questNumber;
		}
	}
}
