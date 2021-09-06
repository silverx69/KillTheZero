using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Movement/Auto Move")]
[RequireComponent(typeof(Rigidbody2D))]
public class AutoMove : Physics2DObject {
	// These are the forces that will push the object every frame
	// don't forget they can be negative too!
	public Vector2 direction = new Vector2(1f, 0f);

	// True to spawn with a random direction
	public bool randomDirection = false;
	public float randomDirectionRange = 2f;

	// is the push relative or absolute to the world?
	public bool relativeToRotation = true;

	private void Start() {
		if (randomDirection)
			direction = new Vector2(
				Random.Range(-randomDirectionRange, randomDirectionRange), 
				Random.Range(-randomDirectionRange, randomDirectionRange));
	}

	// FixedUpdate is called once per frame
	void FixedUpdate() {
		if (relativeToRotation) {
			rigidbody2D.AddRelativeForce(direction * 2f);
		}
		else {
			rigidbody2D.AddForce(direction * 2f);
		}
	}


	//Draw an arrow to show the direction in which the object will move
	void OnDrawGizmosSelected() {
		if (this.enabled) {
			float extraAngle = (relativeToRotation) ? transform.rotation.eulerAngles.z : 0f;
			Utils.DrawMoveArrowGizmo(transform.position, direction, extraAngle);
		}
	}
}
