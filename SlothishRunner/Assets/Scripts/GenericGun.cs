using UnityEngine;
using System.Collections;

public class GenericGun : MonoBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawnPos;
	private bool canShoot;
//	private bool isReloading;
	private float reloadTimer;
//	private float fireRateTimer;
	public float fireRateCooldown;
	public float reloadTime;
	public int totalAmmo;
	private int ammoClip;

	// Use this for initialization
	void Start () {
		canShoot = true;
		//isReloading = false;
		reloadTimer = reloadTime;
		//fireRateTimer = 0;
		ammoClip = totalAmmo;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && canShoot) {
			Shoot ();
		}
	}

	/*void GetFireRateUpdate()
	{
		if (fireRateTimer < fireRateCooldown) {
			fireRateTimer += Time.fixedDeltaTime;
			canShoot = false;
			if(fireRateTimer >= fireRateCooldown)
			{
				canShoot = true;
			}
		}
	}*/

	/*bool GetReloadUpdate()
	{
		if (reloadTimer < reloadTime) {
			reloadTimer += Time.fixedDeltaTime;
			canShoot = false;
		}
	}*/

	void Reload()
	{
		if (ammoClip == totalAmmo) {
			return;
		}
		canShoot = false;
		StartCoroutine ("GunReload");
	}

	IEnumerator GunReload()
	{
		/*while (reloadTimer < reloadTime) {
			reloadTimer += Time.fixedDeltaTime;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}*/
		yield return new WaitForSeconds (reloadTime);
		ammoClip = totalAmmo;
		canShoot = true;

	}

	IEnumerator TimeBetweenShots()
	{
		yield return new WaitForSeconds (fireRateCooldown);
		canShoot = true;
	}

	void Shoot()
	{
		if (ammoClip == 0) {
			Reload ();
		} else {
			ammoClip--;
			GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity) as GameObject;
			newBullet.GetComponent<RocketMissile>().SetDirection(this.transform.forward);
			Debug.DrawLine(this.transform.position, this.transform.forward * 2);
			canShoot = false;
			StartCoroutine("TimeBetweenShots");
		}
	}
}
