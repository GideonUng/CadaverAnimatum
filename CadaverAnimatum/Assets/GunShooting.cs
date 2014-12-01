using UnityEngine;
using System.Collections;

public class GunShooting : MonoBehaviour
{
	public float fireRate;

	public AudioClip reloadClip;
	public AudioClip dryShotClip;

	private bool canShoot = true;

	public bool shoot()
	{
		if(!canShoot)
		{
			return false;
		}

		GameObject.Find("MuzzleFlash").GetComponent<Light>().enabled = true;

		Instantiate(Resources.Load("GW18_Shot_Sound"), transform.position, transform.rotation);
		//GameObject.Find("Camera").GetComponent<MouseLook>().rotationY += 3; 
		//Recoil
				
		StartCoroutine(muzzleFlashOff());
		StartCoroutine(FireRate());
		return true;
	}

	public void dryShot()
	{
		GetComponent<AudioSource>().clip = dryShotClip;
		GetComponent<AudioSource>().Play();
	}
	
	public void reload()
	{
		GetComponent<AudioSource>().clip = reloadClip;
		GetComponent<AudioSource>().Play();
	}
	
	IEnumerator muzzleFlashOff()
	{
		yield return new WaitForSeconds(0.05f);
		if (GameObject.Find("MuzzleFlash") != null)
		{
			GameObject.Find("MuzzleFlash").GetComponent<Light>().enabled = false;
		}
	}
	IEnumerator FireRate()
	{
		canShoot = false;
		yield return new WaitForSeconds(1 / fireRate);
		canShoot = true;
	}
}