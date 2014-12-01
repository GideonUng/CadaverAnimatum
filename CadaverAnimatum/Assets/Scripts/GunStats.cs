using UnityEngine;
using System.Collections;

public class GunStats : MonoBehaviour
{
	public int bulletsInGun;
	public int magSize;
	public bool isPrimaryGun;

	void Start()
	{
		bulletsInGun = Random.Range(0, magSize);
	}
}
