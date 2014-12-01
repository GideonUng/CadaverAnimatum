using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
	public float bodyDisaperTime = 10;
	public Inventory inventory;

	private Transform cameraPosition;

	void Start()
	{
		cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}

	void Update()
	{
		if (!GetComponent<Animator>().GetBool("Dead"))
		{
			if(GetComponent<Inventory>().secondaryGun != null || GetComponent<Inventory>().primaryGun != null)
			{
				if (GetComponent<Animator>().GetBool("Aiming"))
				{
					if (Input.GetButtonDown("Fire"))
					{
						RaycastHit hit;
						bool shot;

						if(inventory.primaryGunActive)
						{
							shot = inventory.primaryGun.GetComponent<GW18Shooting>().shoot();
						} 
						else 
						{
							shot = inventory.secondaryGun.GetComponent<GW18Shooting>().shoot();
						}
						if(shot)
						{
							foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
							{
								if((transform.position - enemy.transform.position).magnitude <= 100)
								{
									enemy.GetComponent<HumanEnemyAI>().shotsFell = true;
								}
							}

							if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit))
							{
								print(hit.collider.gameObject.name);

								//What happens if hit
								if (hit.collider.gameObject.tag == "Enemy")
								{
									Debug.Log("EnemyHit"); 
									hit.collider.gameObject.GetComponent<Animator>().SetBool("Dead", true);
									Destroy(hit.collider.gameObject, bodyDisaperTime);
								}

							}
						}
					}
				}

				if(Input.GetButtonDown("Reload"))
				{
					if(inventory.primaryGunActive)
					{
						if(inventory.primaryGun != null)
						{
							inventory.primaryGun.GetComponent<GW18Shooting>().reload();
						}
					} 
					else 
					{
						if(inventory.secondaryGun != null)
						{
							inventory.secondaryGun.GetComponent<GW18Shooting>().reload();
						}
					}
				}
			}
		}
	}
}