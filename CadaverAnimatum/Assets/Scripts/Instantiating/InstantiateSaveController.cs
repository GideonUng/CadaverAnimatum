using UnityEngine;
using System.Collections;

public class InstantiateSaveController : MonoBehaviour
{
	public Object saveController;
	
	void Awake()
	{
		if (GameObject.Find("Save Controller") == null)
		{
			Instantiate(saveController).name = "Save Controller";
		}
	}
}
