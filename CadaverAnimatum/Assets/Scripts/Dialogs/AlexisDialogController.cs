using UnityEngine;
using System.Collections;

public class AlexisDialogController : MonoBehaviour
{
	public Transform player;
	public bool lookAtPlayer;
	public AudioClip[] clips;
	public float disdanceForward;
	public float disdanceRight;
	public float disdanceUp;

	private GameObject mainCamera;
	private GameObject dialogCamera;
	private Animator playerAnim;
	private Animator alexisAnim;
	private bool isTalking = false;

	void Start()
	{
		mainCamera = GameObject.Find("Camera");
		dialogCamera = GameObject.Find("DialogCamera");
		dialogCamera.transform.LookAt(player.transform.position);
		playerAnim = player.GetComponent<Animator>();
		alexisAnim = GetComponent<Animator>();

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameObject.Find("GUI Text").GetComponent<GUIText>().enabled = true;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (Input.GetButtonUp("Interact"))
			{
				if (!isTalking)
				{
					startDialog();
					isTalking = true;
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameObject.Find("GUI Text").GetComponent<GUIText>().enabled = false;
		}
	}

	IEnumerator dialog()
	{
		GameObject.Find("GUI Text").GetComponent<GUIText>().enabled = false;

		transform.LookAt(player.transform);
		player.transform.position = transform.position + transform.forward * 2;
		player.transform.LookAt(transform);

		player.GetComponent<PlayerMovement>().enabled = false;
		playerAnim.SetFloat("Speed", 0);
		playerAnim.SetBool("Aiming", false);

		mainCamera.camera.enabled = false;
		mainCamera.GetComponent<MouseLook>().enabled = false;
		dialogCamera.camera.enabled = true;

		foreach (AudioClip clip in clips)
		{
			if (lookAtPlayer)
			{
				dialogCamera.transform.position = player.transform.position + player.transform.forward * disdanceForward + Vector3.up * disdanceUp + transform.right * disdanceRight;
				dialogCamera.transform.LookAt(player.transform.position + Vector3.up);

				playerAnim.SetBool("Dialog", true);
			}
			else
			{
				dialogCamera.transform.position = transform.position + transform.forward * disdanceForward + Vector3.up * disdanceUp + player.transform.right * disdanceRight;
				dialogCamera.transform.LookAt(transform.position + Vector3.up);

				alexisAnim.SetBool("Dialog", true);
			}

			audio.clip = clip;
			audio.Play();
			yield return new WaitForSeconds(clip.length - audio.time);

			if (lookAtPlayer)
			{
				playerAnim.SetBool("Dialog", false);
			}
			else
			{
				alexisAnim.SetBool("Dialog", false);
			}

			lookAtPlayer = !lookAtPlayer;
		}

		mainCamera.camera.enabled = true;
		mainCamera.GetComponent<MouseLook>().enabled = true;
		dialogCamera.camera.enabled = false;

		player.GetComponent<PlayerMovement>().enabled = true;

		isTalking = false;
		GameObject.Find("GUI Text").GetComponent<GUIText>().enabled = true;
	}

	public void startDialog()
	{
		StartCoroutine(dialog());
	}
}
