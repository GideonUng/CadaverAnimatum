using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	public Quest quest;

	public bool showQuest;
	public bool showSaved;
	public bool showInteract;

	public Rect ammoArea;
	public Rect questArea;
	public Rect savedArea;
	public Rect interactArea;

	public string interactMessage;

	public float questDisplaytime;

	private Inventory inventory;

	void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}

	void OnGUI()
	{
		GUI.Box(ammoArea, "");
		GUILayout.BeginArea(ammoArea);
		GUILayout.Label("Ammo: " + inventory.secondaryAmmo);
		GUILayout.Label("Bullets: " + inventory.secondaryBulletsInMag);
		GUILayout.Label("FirstAid Kits: " + inventory.firstAidKits);
		GUILayout.EndArea();

		if (showQuest)
		{
			GUILayout.BeginArea(questArea);
			GUILayout.Label(quest.titel);
			GUILayout.Label(quest.description);
			GUILayout.EndArea();
		}

		if (showSaved)
		{
			GUI.Label(savedArea, "Game Saved!");
		}

		if (showInteract)
		{
			GUI.Label(interactArea, interactMessage);
		}
	}

	public void displaySaved()
	{
		StartCoroutine(displaySavedCo());
	}

	public void displayInteract(string message)
	{
		interactMessage = message;
		showInteract = true;
	}

	public void hideInteract()
	{
		showInteract = false;
	}

	public IEnumerator displayQuest()
	{
		yield return new WaitForEndOfFrame();
		quest = GameObject.FindGameObjectWithTag("Quest").GetComponent<Quest>();

		showQuest = true;
		yield return new WaitForSeconds(questDisplaytime);
		showQuest = false;
	}

	public IEnumerator displaySavedCo()
	{
		showSaved = true;
		yield return new WaitForSeconds(questDisplaytime);
		showSaved = false;
	}
}
