using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
	public PlayerMovement playerMovement;

	public bool hasBackpack;

	public GameObject primaryGun;
	public GameObject secondaryGun;
	public SaveDataReference saveDataRef;

	public bool primaryGunActive;

	public int primaryAmmo;
	public int secondaryAmmo;
	public int maxPrimaryAmmo;
	public int maxSecondaryAmmo;

	public int primaryMagSize;
	public int secondaryMagSize;

	public int primaryBulletsInMag;
	public int secondaryBulletsInMag;

	public int firstAidKits;
	public int breads;

	public int molotov;
	public int canGrenade;
	public int nailGrenade;

	public int bottles;
	public int rags;
	public int alcohols;
	public int nails;
	public int cans;
	public int blackPowder;

	public Object[] Guns;

	void Start()
	{
		saveDataRef = GameObject.Find("Save Controller").GetComponent<SaveDataReference>();
	}

	void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			changeWeapon();
		}
	}

	public void pickUpItem(string itemName)
	{
		switch (itemName)
		{
			case "Backpack":
				hasBackpack = true;
				break;
			case "Bottle":
				bottles += 1;
				break;
			case "Rag":
				rags += 1;
				break;
			case "Alcohol":
				alcohols += 1;
				break;
			case "Nail":
				nails += 1;
				break;
			case "Can":
				cans += 1;
				break;
			case "blackPowder":
				blackPowder += 1;
				break;
			case "Medikit":
				firstAidKits += 1;
				saveDataRef.saveData.data.firstAidKits += 1;
				break;
			case "bread":
				breads += 1;
				break;
		}
	}

	public void pickUpGun(string name, int bullets, int magSize, bool isPrimaryGun)
	{
		if(name != "")
		{
			if (isPrimaryGun)
			{
				saveDataRef.saveData.data.primaryGun = name;
				saveDataRef.saveData.data.primaryAmmo = bullets;
				saveDataRef.saveData.data.primaryMagSize = magSize;
			}
			else
			{
				saveDataRef.saveData.data.secondaryGun = name;
				saveDataRef.saveData.data.secondaryAmmo = bullets;
				saveDataRef.saveData.data.secondaryMagSize = magSize; 
			}

			if (name != "")
			{
				if (isPrimaryGun == primaryGunActive)
				{
					playerMovement.canAim = true;
				}
				GameObject gunHolder = GameObject.FindGameObjectWithTag("GunHolder");

				if (!isPrimaryGun)
				{
					secondaryAmmo += bullets;
					GameObject currentGun = GameObject.FindGameObjectWithTag("SecondaryGun");

					if (secondaryGun == null || (secondaryGun != null && currentGun.name != name))
					{
						Destroy(currentGun);
						switch (name)
						{
							case "GW18":
								GameObject gun = Instantiate(Guns[0]) as GameObject;
								gun.transform.parent = gunHolder.transform;
								gun.transform.localPosition = new Vector3(-0, 0, 0);
								gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
								gun.name = "GW18";
								secondaryGun = gun;

								secondaryMagSize = magSize;
								if (primaryGunActive)
								{
									gun.SetActive(false);
								}
								break;
						}
					}
				}
				else
				{
					primaryAmmo += bullets;
					GameObject currentGun = GameObject.FindGameObjectWithTag("PrimaryGun");

					if (primaryGun == null || (primaryGun != null && currentGun.name != name))
					{
						Destroy(currentGun);
						switch (name)
						{
							case "GW30":
								GameObject gun = Instantiate(Guns[1]) as GameObject;
								gun.transform.parent = gunHolder.transform;
								gun.transform.localPosition = new Vector3(-0, 0, 0);
								gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
								gun.name = "GW30";
								primaryGun = gun;

								primaryMagSize = magSize;

								if (!primaryGunActive)
								{
									gun.SetActive(false);
								}
								break;
						}
					}
				}
				}
			}
	}

	public void pickUpAmmo(int bullets, bool isPrimaryGun)
	{
		if (isPrimaryGun)
		{
			primaryAmmo += bullets;
		}
		else
		{
			secondaryAmmo += bullets;
		}
	}

	public void changeWeapon()
	{
		primaryGunActive = !primaryGunActive;
		GetComponent<Animator>().SetBool("HoldsPrimaryGun", primaryGunActive);

		if (primaryGunActive)
		{
			if (secondaryGun != null)
			{
				secondaryGun.SetActive(false);
			}

			if (primaryGun != null)
			{
				playerMovement.canAim = true;
				primaryGun.SetActive(true);
			}
			else
			{
				playerMovement.canAim = false;
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
				playerMovement.canAim = true;
			}
			else
			{
				playerMovement.canAim = false;
			}
		}
	}
}