using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] private Transform EffectSpawnPoint;
	[SerializeField] private GameObject BulletEfect;
 
	public GameObject hitParticle;
	private float speed = 5f;
	float shootTimer = 0;

	private Rigidbody rb;
	private AudioSource shootSound;

	void Start () {
		Application.targetFrameRate = 300;
		shootSound = GameObject.Find("ShotSound").GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		Vector3 moveX = JoystickLeft.positionX * speed * transform.right;
		Vector3 moveY = JoystickLeft.positionY * speed * transform.forward;
		rb.MovePosition(transform.position + moveX * Time.fixedDeltaTime + moveY  * Time.fixedDeltaTime);

		//transform.position = transform.position + moveX * Time.fixedDeltaTime + moveY  * Time.fixedDeltaTime; If your character doesn't have rigidbody you can use this
	}

	void Update() {
		transform.rotation = Quaternion.Euler(JoystickRight.rotY, JoystickRight.rotX, 0);

		shootTimer += Time.deltaTime;
		if(JoystickRight.shot) {
			if(shootTimer >= 0.2f) {
				shootTimer = 0;
				shootSound.Play();
				ShootBullets();
			}
		}
	}

	private void ShootBullets() {
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit)) {
			GameObject particle = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(particle, 0.5f);
			GameObject d = Instantiate(BulletEfect, EffectSpawnPoint.position, Quaternion.identity);
			d.transform.LookAt(hit.point);
		}
	}

	public void Jump() {
		if(JoystickRight.jump) {
			JoystickRight.jump = false;
			GetComponent<Rigidbody> ().AddForce(new Vector3(0, 300, 0));	
		}
	}

	void OnTriggerEnter(Collider col) {
		JoystickRight.jump = true;
	}

}
