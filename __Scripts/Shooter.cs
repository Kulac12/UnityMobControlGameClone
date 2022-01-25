using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	// ates etmek icin prpject prefabi
	public GameObject projectile;
	public float power = 10.0f;
	
	// atis sesi
	public AudioClip shootSFX;
	
	// Update is called once per frame
	void Update () {
		// space tusu veya farenin sağ tusuna basilinca
		if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
		{	
			// projectile tanimli ise
			if (projectile)
			{
				
				GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation) as GameObject;

				// merminin rigidbody si yoksa eklemek için (yer çekimi için)
				if (!newProjectile.GetComponent<Rigidbody>()) 
				{
					newProjectile.AddComponent<Rigidbody>();
				}
				// bir tane Rigidbody e sahipse 
				newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);
				
				// atis edildiginde ses calmasi icin
				/*if (shootSFX)
				{
					if (newProjectile.GetComponent<AudioSource> ()) { 
						newProjectile.GetComponent<AudioSource> ().PlayOneShot (shootSFX);
					} else {
						

						AudioSource.PlayClipAtPoint (shootSFX, newProjectile.transform.position);
					}
				}*/
			}
		}
	}
}
