using UnityEngine;
using System.Collections;

public class HumanEnemyShooting : MonoBehaviour 
{
	public float spray;

	public Transform shootingPos;
	public HumanEnemyInventory inventory;
	
	public void shoot()
	{
		RaycastHit hit;
		bool shot;

		if(inventory.primaryGunActive)
		{ 
			shot = inventory.primaryGun.GetComponent<GunShooting>().shoot();
		} 
		else 
		{
			shot = inventory.secondaryGun.GetComponent<GunShooting>().shoot();
		}

		if(shot)
		{
			Vector3 sprayVe = new Vector3(Random.Range(-spray, spray), Random.Range(-spray, spray), Random.Range(-spray, spray));

			if (Physics.Raycast(shootingPos.position, shootingPos.forward + sprayVe, out hit))
			{
				print(hit.collider.gameObject.name);
				Debug.DrawLine(shootingPos.position, hit.point, Color.red, 9f);

				//What happens if hit
				if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Friend")
				{
					Debug.Log("Player Hit"); 
					hit.collider.gameObject.GetComponent<Animator>().SetBool("Dead", true);
				}
			}
		}
	}

	public void reload()
	{
		int bulletsToBeReloaded;

		if(inventory.primaryGunActive)
		{
			if(inventory.primaryGun != null)
			{
				bulletsToBeReloaded = (inventory.primaryMagSize - inventory.primaryBulletsInMag);

				if (bulletsToBeReloaded > inventory.primaryAmmo)
				{
					bulletsToBeReloaded -= bulletsToBeReloaded - inventory.primaryAmmo;
				}
				
				inventory.primaryAmmo -= bulletsToBeReloaded;
				inventory.primaryBulletsInMag += bulletsToBeReloaded;

				if (bulletsToBeReloaded != 0)
				{
					inventory.primaryGun.GetComponent<GunShooting>().reload();	
				}
			}
		} 
		else 
		{
			if(inventory.secondaryGun != null)
			{
				bulletsToBeReloaded = (inventory.secondaryMagSize - inventory.secondaryBulletsInMag);

				if (bulletsToBeReloaded > inventory.secondaryAmmo)
				{
					bulletsToBeReloaded -= bulletsToBeReloaded - inventory.secondaryAmmo;
				}
				
				inventory.secondaryAmmo -= bulletsToBeReloaded;
				inventory.secondaryBulletsInMag += bulletsToBeReloaded;
				
				if (bulletsToBeReloaded != 0)
				{
					inventory.secondaryGun.GetComponent<GunShooting>().reload();	
				}			
			}
		}
	}
}