using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2DController : MonoBehaviour {

	float speedForce = 20f;
	float torqueForce = 150f;
	float driftFactor = 0.9f;

	public Transform targetLeft;
	public Transform targetRight;
	public Transform targetUp;

	public RaycastHit2D leftHit;
	public float lDist;

	public RaycastHit2D rightHit;
	public float rDist;

	public RaycastHit2D upHit;
	public float uDist;

	// Use this for initialization
	void Start () {

	}

	void Update() {

	}

	// Update is called once per frame
	void FixedUpdate () {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if (Input.GetButton("Acc")) {
			rb.AddForce(transform.up * speedForce);
		}
		rb.angularVelocity = Input.GetAxis("Horizontal") * torqueForce;
		rb.velocity = ForwardVelocity() + RightVelocity() * driftFactor;
		if (Input.GetButton("Brake")) {
			rb.velocity = new Vector2(0, 0);
		}

		if (transform.position.x<0) {
			leftHit = Physics2D.Raycast(targetLeft.position, targetLeft.TransformDirection(new Vector2 (targetLeft.position.x, Mathf.Tan ((0) * .5f * Mathf.Deg2Rad) * targetLeft.position.x)) );
			rightHit = Physics2D.Raycast(targetRight.position, targetRight.TransformDirection(new Vector2 (-targetRight.position.x, Mathf.Tan ((0) * .5f * Mathf.Deg2Rad) * -targetRight.position.x)) );
			upHit = Physics2D.Raycast(targetUp.position, targetUp.TransformDirection(new Vector2 (targetUp.position.x, Mathf.Tan ((180) * .5f * Mathf.Deg2Rad) * targetUp.position.x)) );
		} else {
			leftHit = Physics2D.Raycast(targetLeft.position, targetLeft.TransformDirection(new Vector2 (-targetLeft.position.x, Mathf.Tan ((0) * .5f * Mathf.Deg2Rad) * -targetLeft.position.x)) );
			rightHit = Physics2D.Raycast(targetRight.position, targetRight.TransformDirection(new Vector2 (targetRight.position.x, Mathf.Tan ((0) * .5f * Mathf.Deg2Rad) * targetRight.position.x)) );
			upHit = Physics2D.Raycast(targetUp.position, targetUp.TransformDirection(new Vector2 (-targetUp.position.x, Mathf.Tan ((180) * .5f * Mathf.Deg2Rad) * -targetUp.position.x)) );
		}

		Debug.DrawLine(targetLeft.position,leftHit.point,Color.white);
		lDist = (leftHit.point - (Vector2) targetLeft.position).magnitude;
		Debug.DrawLine(targetRight.position,rightHit.point,Color.white);
		rDist = (rightHit.point - (Vector2) targetRight.position).magnitude;
		Debug.DrawLine(targetUp.position,upHit.point,Color.white);
		uDist = (upHit.point - (Vector2) targetUp.position).magnitude;
		print("Left: "+lDist);
		print("Right: "+rDist);
		print("Up: "+uDist);
	}

	Vector2 ForwardVelocity() {
		return transform.up * Vector2.Dot( GetComponent<Rigidbody2D>().velocity, transform.up );
	}

	Vector2 RightVelocity() {
		return transform.right * Vector2.Dot( GetComponent<Rigidbody2D>().velocity, transform.right );
	}
}
