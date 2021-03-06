using UnityEngine;
using System.Collections;

public class Hero : Transform {

	public float speedH = 2.0f;
	public float speedV = 2.0f;
	public float movementSpeed = 5.0f;

	public Transform head;
	public Transform body;
	
	float yaw = 0.0f;
	float pitch = 0.0f;

	protected Weapon leftWeapon;
	protected Weapon rightWeapon;

	private Weapon lastShot;

	protected readonly Ability[] abilities = new Ability[4];

	protected void shoot() {

		if (leftWeapon != null && rightWeapon != null) {
			//Dual wielding
			if (lastShot == null) {
				lastShot = rightWeapon;
				rightWeapon.shoot();
			} else if (lastShot == leftWeapon) {
				lastShot = rightWeapon;
				rightWeapon.shoot();
			} else if (lastShot == rightWeapon) {
				lastShot = leftWeapon;
				leftWeapon.shoot();
			}
		} else if (leftWeapon != null) {
			//Single weapon
			leftWeapon.shoot();
		} else if (rightWeapon != null) {
			//Single weapon
			rightWeapon.shoot();
		} else {
			//No weapon
		}

	}

	void Update() {
		
		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

		yaw %= 360;
		pitch %= 360;

		body.eulerAngles = Vector3.up * yaw;
		head.eulerAngles = new Vector3(pitch, yaw, 0);

		Vector3 pos = Vector3.zero;
		
		pos += movementSpeed * body.forward * Time.deltaTime * Input.GetAxis("Horizontal");
		pos += movementSpeed * body.right * Time.deltaTime * Input.GetAxis("Vertical");
		
		this.position += pos;

		if (Input.GetMouseButtonDown(0)) {
			//Left Mouse button
			shoot();
		}

		if (Input.GetKey (KeyCode.Alpha1)) {
			abilities[0].trigger();
		}
		if (Input.GetKey (KeyCode.Alpha2)) {
			abilities[1].trigger();
		}
		if (Input.GetKey (KeyCode.Alpha3)) {
			abilities[2].trigger();
		}
		if (Input.GetKey (KeyCode.Alpha4)) {
			abilities[3].trigger();
		}

	}
	
	void Start () {
		
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		
	}

}
