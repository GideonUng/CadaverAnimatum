using UnityEngine;
using System.Collections;

public class GW18Shooting : MonoBehaviour
{
	public Inventory inventory;
	public AudioClip reloadClip;
	public AudioClip dryShotClip;
	public SaveData saveData;

	void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		saveData = GameObject.Find("Save Controller").GetComponent<SaveDataReference>().saveData;
	}

	public bool shoot()
	{
		if (inventory.primaryGunActive)
		{
			if (inventory.primaryBulletsInMag != 0)
			{
				GameObject.Find("Eleanor").GetComponent<Animator>().SetBool("Shooting", true);
				GameObject.Find("MuzzleFlash").GetComponent<Light>().enabled = true;

				inventory.primaryBulletsInMag -= 1;
				saveData.data.primaryBulletsInMag -= 1;

				Instantiate(Resources.Load("GW18_Shot_Sound"), transform.position, transform.rotation);
				GameObject.Find("Camera").GetComponent<MouseLook>().rotationY += 3;

				StartCoroutine(muzzleFlashOff());
				return true;
			}
			else
			{
				GetComponent<AudioSource>().clip = dryShotClip;
				GetComponent<AudioSource>().Play();
				return false;
			}
		}
		else
		{

			if (inventory.secondaryBulletsInMag != 0)
			{
				GameObject.Find("Eleanor").GetComponent<Animator>().SetBool("Shooting", true);
				GameObject.Find("MuzzleFlash").GetComponent<Light>().enabled = true;

				inventory.secondaryBulletsInMag -= 1;
				saveData.data.secondaryBulletsInMag -= 1;

				Instantiate(Resources.Load("GW18_Shot_Sound"), transform.position, transform.rotation);
				GameObject.Find("Camera").GetComponent<MouseLook>().rotationY += 3;

				StartCoroutine(muzzleFlashOff());
				return true;
			}
			else
			{
				GetComponent<AudioSource>().clip = dryShotClip;
				GetComponent<AudioSource>().Play();
				return false;
			}
		}
	}

	public void reload()
	{
		int bulletsToBeReloaded;

		if (inventory.primaryGunActive)
		{
			bulletsToBeReloaded = (inventory.primaryMagSize - inventory.primaryBulletsInMag);
			if (bulletsToBeReloaded > inventory.primaryAmmo)
			{
				bulletsToBeReloaded -= bulletsToBeReloaded - inventory.primaryAmmo;
			}

			inventory.primaryAmmo -= bulletsToBeReloaded;
			inventory.primaryBulletsInMag += bulletsToBeReloaded;

			saveData.data.primaryAmmo -= bulletsToBeReloaded;
			saveData.data.primaryBulletsInMag += bulletsToBeReloaded;

			if (bulletsToBeReloaded != 0)
			{
				GetComponent<AudioSource>().clip = reloadClip;
				GetComponent<AudioSource>().Play();
			}
		}
		else
		{
			bulletsToBeReloaded = (inventory.secondaryMagSize - inventory.secondaryBulletsInMag);
			if (bulletsToBeReloaded > inventory.secondaryAmmo)
			{
				bulletsToBeReloaded -= bulletsToBeReloaded - inventory.secondaryAmmo;
			}

			inventory.secondaryAmmo -= bulletsToBeReloaded;
			inventory.secondaryBulletsInMag += bulletsToBeReloaded;

			saveData.data.secondaryAmmo -= bulletsToBeReloaded;
			saveData.data.secondaryBulletsInMag += bulletsToBeReloaded;

			if (bulletsToBeReloaded != 0)
			{
				GetComponent<AudioSource>().clip = reloadClip;
				GetComponent<AudioSource>().Play();
			}
		}
	}

	IEnumerator muzzleFlashOff()
	{
		yield return new WaitForSeconds(0.05f);
		if (GameObject.Find("MuzzleFlash") != null)
		{
			GameObject.Find("MuzzleFlash").GetComponent<Light>().enabled = false;
		}

	}
}