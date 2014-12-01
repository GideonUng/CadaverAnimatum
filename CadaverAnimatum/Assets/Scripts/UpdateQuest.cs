using UnityEngine;
using System.Collections;

public class UpdateQuest : MonoBehaviour
{
	public GameObject newQuest;

	void OnTriggerEnter()
	{
		Destroy(GameObject.FindGameObjectWithTag("Quest"));
		Instantiate(newQuest);
	}
}
