using UnityEngine;
using System.Collections;

public class ApplySaveData : MonoBehaviour
{
	public GameObject player;
	public SavePoints savePoints;
	public SaveDataReference saveDataRef;
	public Inventory inventory;
	public EnableQuest enableQuest;

	void Start()
	{
		saveDataRef = GameObject.Find("Save Controller").GetComponent<SaveDataReference>();

		if (saveDataRef.saveData.data.lastSavePoint != 0)
		{
			player.transform.position = savePoints.savePoints[saveDataRef.saveData.data.lastSavePoint - 1].position;
		}

		enableQuest.enabelQuest(saveDataRef.saveData.data.quest);

		if (saveDataRef.saveData.data.hasBackpack)
		{
			player.GetComponent<PlayerItemPickUp>().backbackPickup();
		}

		inventory.hasBackpack = saveDataRef.saveData.data.hasBackpack;

		inventory.pickUpGun(saveDataRef.saveData.data.primaryGun, saveDataRef.saveData.data.primaryAmmo, saveDataRef.saveData.data.primaryMagSize, true);
		inventory.pickUpGun(saveDataRef.saveData.data.secondaryGun, saveDataRef.saveData.data.secondaryAmmo, saveDataRef.saveData.data.secondaryMagSize, false);

		inventory.primaryGunActive = saveDataRef.saveData.data.primaryGunActive;

		inventory.primaryAmmo = saveDataRef.saveData.data.primaryAmmo;
		inventory.secondaryAmmo = saveDataRef.saveData.data.secondaryAmmo;
		inventory.maxPrimaryAmmo = saveDataRef.saveData.data.maxPrimaryAmmo;
		inventory.maxSecondaryAmmo = saveDataRef.saveData.data.maxSecondaryAmmo;

		inventory.primaryMagSize = saveDataRef.saveData.data.primaryMagSize;
		inventory.secondaryMagSize = saveDataRef.saveData.data.secondaryMagSize;

		inventory.primaryBulletsInMag = saveDataRef.saveData.data.primaryBulletsInMag;
		inventory.secondaryBulletsInMag = saveDataRef.saveData.data.secondaryBulletsInMag;

		inventory.firstAidKits = saveDataRef.saveData.data.firstAidKits;
		inventory.breads = saveDataRef.saveData.data.breads;

		inventory.molotov = saveDataRef.saveData.data.molotov;
		inventory.canGrenade = saveDataRef.saveData.data.canGrenade;
		inventory.nailGrenade = saveDataRef.saveData.data.nailGrenade;

		inventory.bottles = saveDataRef.saveData.data.bottles;
		inventory.rags = saveDataRef.saveData.data.rags;
		inventory.alcohols = saveDataRef.saveData.data.alcohols;
		inventory.nails = saveDataRef.saveData.data.nails;
		inventory.cans = saveDataRef.saveData.data.cans;
		inventory.blackPowder = saveDataRef.saveData.data.blackPowder;
	}
}
