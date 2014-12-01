using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour 
{
	public Transform shootingpos;

	public HumanEnemyInventory inventory;

	void Start()
	{
		inventory = GetComponent<HumanEnemyInventory>();
	}

	public void shoot()
	{
		RaycastHit hit;

		inventory.secondaryGun.GetComponent<GunShooting>().shoot();

		if(true)
		{
			if (Physics.Raycast(shootingpos.position, shootingpos.forward, out hit))
			{
				print(hit.collider.gameObject.name);
				
				//What happens if hit
				if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Friend")
				{
					Debug.Log(hit.collider.gameObject.tag + " Hit"); 
					hit.collider.gameObject.GetComponent<Animator>().SetBool("Dead", true);
				}
			}
		}
	}

	public void reload()
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
