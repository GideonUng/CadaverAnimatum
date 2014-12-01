using UnityEngine;
using System.Collections;

public class CreditsGUI : MonoBehaviour 
{
	public string[] people;

	public GUIStyle centerStyle;

	void Start()
	{
		Screen.showCursor = true;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("Menu");
		}
	}

	float creditPos;

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0, Screen.height - creditPos, Screen.width, Screen.height));

		foreach (string pepl in people)
		{
			GUILayout.Label(pepl, centerStyle);
		}

		GUILayout.EndArea();

		if(creditPos <= Screen.height - 100)
		{
			creditPos += 0.5f;
		}
	}
}
