
using UnityEngine;
using System.Collections;

public class RocketMissile : MonoBehaviour {
	public float radius;
	public float power;
	public GameObject RocketPrefab;
	public Vector3 direction;
	public float speed;

	/*RocketMissile(Vector3 direction)
	{
		this.transform = direction;
	}*/

	public void SetDirection(Vector3 _direction)
	{
		direction = _direction;
	}
	
	void Start() {
		StartCoroutine ("SelfDestroy");
	}

	void Update() {
		this.transform.Translate (direction * speed);
	}

	void OnCollisionEnter(Collision col)
	{
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		if (colliders.Length == 0) {
			return;
		}
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			
			if (rb != null){
				rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
				Debug.Log ("RigidBodyFound, Adding Force");
			}
			
		}

		Destroy (this.gameObject);
	}

	IEnumerator SelfDestroy()
	{
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
	}
}