using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour
{
	public string titel;
	public string description;
	public HUD hud;
	
	public bool showQuest;
	private Rect QuestGUIRect;

	void Start()
	{
		SaveLoad.Save(false);
		hud = GameObject.Find("Camera").GetComponent<HUD>();
		StartCoroutine(hud.displayQuest());
	}
}
