using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//düsþman askerleri player tagine sahip þeylere hasar vermesi için -- kendi kendilerini öldürmesinler diye.
public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 10.0f;

    public bool damageOnTrigger = true;
    public bool damageOnCollision = false;
    public bool continuousDamage = false;
    public float continuousTimeBetweenHits = 0;
    public float continuousDamageTime = 0;
    public bool destroySelfOnImpact = false;    
    public float delayBeforeDestroy = 0.0f;
    public GameObject explosionPrefab;

    private float savedTime = 0;



	void OnTriggerEnter(Collider collision)         // tetikleyici olan mermiler gibi þeyler için
	{
		if (damageOnTrigger)
		{
			if (this.tag == "EnemyBullet" && collision.gameObject.tag == "EnemyKule") // Düþman kendi mermileri ile kendini vurduysa bu durumu yoksaymak için
				return;

			if (collision.gameObject.GetComponent<Health>() != null)
			{   // vurulan gameObjectin üstünde "Health" scripti varsa
				collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);

				if (destroySelfOnImpact)
				{
					Destroy(gameObject, delayBeforeDestroy);      // bir þeye çarptýðýnda nesneyi ortadan kaldýr.
				}

				if (explosionPrefab != null)
				{
					Instantiate(explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionEnter(Collision collision)
	//Bu çarpýþma anýnýnda patlayan þeylerin tetiklenmemsi için kullanýlýr.
	{
		if (damageOnCollision)
		{
			if (this.tag == "EnemyBullet" && collision.gameObject.tag == "EnemyKule") 
				return;

			if (collision.gameObject.GetComponent<Health>() != null)
			{   
				collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);

				if (destroySelfOnImpact)
				{
					Destroy(gameObject, delayBeforeDestroy);      
				}

				if (explosionPrefab != null)
				{
					Instantiate(explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionStay(Collision collision) // süreç içinde hasar vermek için kullanýlýr (zehirlenme gibi) 
	{
		if (continuousDamage)
		{
			if (collision.gameObject.tag == "EnemyKule" && collision.gameObject.GetComponent<Health>() != null)
			{   // vurulduðu þey sadece "EnemyKule" ise tetiklenir.
				if (Time.time - savedTime >= continuousTimeBetweenHits)
				{
					savedTime = Time.time;
					collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);
				}
			}
		}
	}

}
