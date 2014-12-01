using UnityEngine;
using System.Collections;

public class HumanEnemyInventory : MonoBehaviour 
{
	public GameObject primaryGun;
	public GameObject secondaryGun;

	public bool primaryGunActive;
	
	public int primaryAmmo;
	public int secondaryAmmo;
	
	public int primaryMagSize;
	public int secondaryMagSize;
	
	public int primaryBulletsInMag;
	public int secondaryBulletsInMag;

	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	public void changeWeapon()
	{
		primaryGunActive = !primaryGunActive;
		anim.SetBool("HoldsPrimaryGun", primaryGunActive);
		
		if (primaryGunActive)
		{
			if (secondaryGun != null)
			{
				secondaryGun.SetActive(false);
			}
			
			if (primaryGun != null)
			{
				primaryGun.SetActive(true);
			}
		}
		else
		{
			if (primaryGun != null)
			{
				primaryGun.SetActive(false);
			}
			
			if (secondaryGun != null)
			{
				secondaryGun.SetActive(true);
			}
		}
	}
}
