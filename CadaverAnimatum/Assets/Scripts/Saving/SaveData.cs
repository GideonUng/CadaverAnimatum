using UnityEngine;
using System.Collections;

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable()]
public struct Data
{
	public int currentLevel;

	public int lastSavePoint;

	public int quest;

	public bool escapeCampFirstMonoDone;

	// Inventory
	public bool hasBackpack;

	public string primaryGun;
	public string secondaryGun;

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
}

[Serializable()]
public class SaveData : ISerializable
{
    public Data data;
	int i;

	public SaveData()
	{

	}

	public SaveData(SerializationInfo info, StreamingContext ctxt)
	{
		data = (Data)info.GetValue("Auto", typeof(Data));
	}

	// Required by the ISerializable class to be properly serialized. This is called automatically
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("Auto", data);
	}
}

