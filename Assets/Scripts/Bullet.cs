using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	/*int controllerID;

	Rigidbody2D rb;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void SetUp(int _controllerID, float angle) {
		controllerID = _controllerID;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		rb.velocity = transform.right * 30f;
		Destroy(gameObject, 5f);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if(!collision.attachedRigidbody) {
			return;
		}
		PlayerController playerController = collision.attachedRigidbody.GetComponent<PlayerController>();
		if(playerController) {
			if(playerController.OnTakeDamage(50f, controllerID))
				Destroy(gameObject);
		}
	}*/
}