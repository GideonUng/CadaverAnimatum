using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour
{
	public GameObject player;
	public GameObject mainCamera;
	public GameObject dialogCamera;
	public GameObject door;

	public bool closed;
	public string closedDisplayText;

	public Transform dialogCamera1;
	public Transform dialogCamera2;

	public AnimationClip open;
	public AnimationClip close;

	public HUD hud;

	public bool inside;
	public bool lookAtPlayer;

	void Start()
	{
		hud = GameObject.Find("Camera").GetComponent<HUD>();
	}

	void LateUpdate()
	{
		if (lookAtPlayer)
		{
			dialogCamera.transform.LookAt(player.transform.position + Vector3.up);
		}

	}

	void OnTriggerStay(Collider other)
	{
		if (other.collider.gameObject.tag == "Player")
		{
			if(closed)
			{
				hud.displayInteract(closedDisplayText);
			} 
			else 
			{
				hud.displayInteract("press E to open door");
				
				if (Input.GetButtonDown("Interact"))
				{
					StartCoroutine(goThroughDoor());
				}
			}
		}
	}

	void OnTriggerExit()
	{
		hud.hideInteract();
	}

	IEnumerator goThroughDoor()
	{
		player.GetComponent<PlayerMovement>().enabled = false;
		player.GetComponent<Animator>().SetFloat("Speed", 0);

		mainCamera.SetActive(false);
		dialogCamera.SetActive(true);

		lookAtPlayer = true;

		player.transform.position = transform.position;
		player.transform.rotation = transform.rotation;

		dialogCamera.transform.position = dialogCamera1.position;
		dialogCamera.transform.rotation = dialogCamera1.rotation;

		door.animation.Play(open.name);


		yield return new WaitForSeconds(0.5f);

		player.GetComponent<Animator>().SetFloat("Speed", 1);

		yield return new WaitForSeconds(1);

		dialogCamera.transform.position = dialogCamera2.position;
		dialogCamera.transform.rotation = dialogCamera2.rotation;

		yield return new WaitForSeconds(1);

		player.GetComponent<Animator>().SetFloat("Speed", 0);

		door.animation.Play(close.name);

		lookAtPlayer = false;

		mainCamera.SetActive(true);
		dialogCamera.SetActive(false);

		player.GetComponent<PlayerMovement>().enabled = true;
	}
}
