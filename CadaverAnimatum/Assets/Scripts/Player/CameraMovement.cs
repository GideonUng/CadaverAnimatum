using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public GameObject crosshair;

	public float distance;
	public float height;
	public float aimingDistance;
	public float aimingHeight;
	public float aimingRight;
	public float zoom;

	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		Screen.showCursor = false;
	}

	public void Update()
	{
		if (player.GetComponent<PlayerMovement>().canAim)
		{
			if (Input.GetButtonDown("Aim"))
			{
				GetComponent<Camera>().fieldOfView = zoom;
				GetComponent<MouseLook>().sensitivityX = 7;
				GetComponent<MouseLook>().sensitivityY = 7;
				crosshair.SetActive(true);
			}

			if (Input.GetButton("Aim"))
			{
				if (transform.rotation.eulerAngles.x < 180)
				{
					player.GetComponent<Animator>().SetFloat("Aim_Angle", transform.rotation.eulerAngles.x);
				}
				else
				{
					player.GetComponent<Animator>().SetFloat("Aim_Angle", transform.rotation.eulerAngles.x - 360);
				}
			}

			if (Input.GetButtonUp("Aim"))
			{
				GetComponent<Camera>().fieldOfView = 60;
				GetComponent<MouseLook>().sensitivityX = 15;
				GetComponent<MouseLook>().sensitivityY = 15;
				crosshair.SetActive(false);
			}
		}
	}

	public void LateUpdate()
	{
		if (player.GetComponent<Animator>().GetBool("Aiming"))
		{
			transform.position = player.transform.position + player.transform.up * aimingHeight - transform.forward * aimingDistance + transform.right * aimingRight;

		}
		else
		{
			transform.position = player.transform.position + player.transform.up * height - transform.forward * distance;

			RaycastHit hit;

			Physics.Raycast(transform.position, player.transform.position + player.transform.up * 1.3f - transform.position, out hit);

			if (hit.collider.gameObject.tag != "Player")
			{
				Physics.Raycast(hit.transform.position, -transform.forward, out hit);

				transform.position = transform.position + transform.forward;
			}
		}

	}
}