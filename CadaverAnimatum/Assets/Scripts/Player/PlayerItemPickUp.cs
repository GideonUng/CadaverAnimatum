using UnityEngine;
using System.Collections;

public class PlayerItemPickUp : MonoBehaviour
{
	public GameObject playerEyes;
	public GameObject backpack;

	public Inventory inventory;
	public Animator anim;

	public float pickUpDistance;
	public bool showLabel;

	private GameObject mainCamera;
	private HUD hud;

	private SaveDataReference saveDataRef;

	private RaycastHit hit;

	void Start()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		hud = mainCamera.GetComponent<HUD>();

		saveDataRef = GameObject.Find("Save Controller").GetComponent<SaveDataReference>();
	}

	void FixedUpdate()
	{
		Physics.Raycast(playerEyes.transform.position, mainCamera.transform.forward, out hit, pickUpDistance);
		Debug.DrawRay(playerEyes.transform.position, mainCamera.transform.forward, Color.green, pickUpDistance);
	}

	void Update()
	{
		showLabel = false;

		if (hit.collider != null)
		{
			switch (hit.collider.gameObject.tag)
			{
				case "Backpack":
					hud.displayInteract("take your Backpack");
					StartCoroutine(hideInteract());

					if (Input.GetButtonUp("Interact"))
					{
						backbackPickup();
					}
					break;

				case "InventoryItem":
					hud.displayInteract("Pick up: " + hit.collider.gameObject.name);
					StartCoroutine(hideInteract());

					if (Input.GetButtonUp("Interact"))
					{
						StartCoroutine(pickUpItem(hit.collider.gameObject));
					}
					break;

				case "GunAtGround":
					hud.displayInteract("Pick up: " + hit.collider.gameObject.name);
					StartCoroutine(hideInteract());

					if (Input.GetButtonUp("Interact"))
					{
						GameObject gun = hit.collider.gameObject;
						inventory.pickUpGun(gun.name, gun.GetComponent<GunStats>().bulletsInGun, gun.GetComponent<GunStats>().magSize, false);
						Destroy(gun);
					}
					break;

				default:
					break;
			}
		}

		if (showLabel)
		{
		}

	}

	IEnumerator hideInteract()
	{
		yield return new WaitForEndOfFrame();
		hud.hideInteract();
	}

	IEnumerator pickUpItem(GameObject item)
	{
		anim.SetBool("BendDown", true);
		GetComponent<PlayerMovement>().enabled = false;
		yield return new WaitForSeconds(1f);
		inventory.pickUpItem(item.name);
		Destroy(item);
		anim.SetBool("BendDown", false);
		yield return new WaitForSeconds(0.5f);
		GetComponent<PlayerMovement>().enabled = true;
	}

	public void backbackPickup()
	{
		backpack.SetActive(true);
		inventory.pickUpItem("Backpack");
		saveDataRef.saveData.data.hasBackpack = true;

		if (GameObject.FindGameObjectWithTag("Backpack") != null)
		{
			Destroy(GameObject.FindGameObjectWithTag("Backpack"));
		}
	}
}
