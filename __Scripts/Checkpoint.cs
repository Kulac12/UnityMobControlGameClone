using UnityEngine;
using System.Collections;


public class Checkpoint : MonoBehaviour {
	
	// Update is called once per frame
	void OnTriggerEnter(Collider collision)						
	{
		if ((collision.gameObject.tag == "DostKule") && (collision.gameObject.GetComponent<Health> () != null))
		{
			collision.gameObject.GetComponent<Health>().updateRespawn(collision.gameObject.transform.position, collision.gameObject.transform.rotation);
		}
	}
}
