using UnityEngine;
using System.Collections;

public class rollCredits : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Application.LoadLevel("Credits");
		}
	}
}
