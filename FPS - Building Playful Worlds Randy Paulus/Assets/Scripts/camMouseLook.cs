using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour {

	public float mouseSensitivity = 5.0f;
	public float mouseSmoothing = 2.0f;
	Vector2 mouseLook;
	Vector2 smoothV;

	GameObject PlayerCapsule;

	// Use this for initialization
	void Start () {
		
		PlayerCapsule = this.transform.parent.gameObject;	
	}
	
	// Update is called once per frame
	void Update () {

		//Gets mousePosition
		var mouseDelta = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		//Smooths mouse
		mouseDelta = Vector2.Scale (mouseDelta, new Vector2 (mouseSensitivity * mouseSmoothing, mouseSensitivity * mouseSmoothing));
		smoothV.x = Mathf.Lerp (smoothV.x, mouseDelta.x, 1f / mouseSmoothing);
		smoothV.y = Mathf.Lerp (smoothV.y, mouseDelta.y, 1f / mouseSmoothing);
		mouseLook += smoothV;

		//Clamps mouse rotation to prevent overrotation.
		mouseLook.y = Mathf.Clamp (mouseLook.y, -90f, 90f);

		//Player and camera follow cursor
		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		PlayerCapsule.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, PlayerCapsule.transform.up);
	}


}
