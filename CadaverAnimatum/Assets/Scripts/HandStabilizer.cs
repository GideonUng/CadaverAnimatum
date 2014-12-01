using UnityEngine;
using System.Collections;

public class HandStabilizer : MonoBehaviour 
{
	public GameObject player;
	public Transform mainCamera;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}

	void LateUpdate()
	{
		if(player.GetComponent<Animator>().GetBool("Aiming")){
			transform.rotation = mainCamera.rotation * Quaternion.Euler(0, -90, -180);
		}
	}
}
