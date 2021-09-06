using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Attributes/Modify Health")]
public class ModifyHealthAttribute : MonoBehaviour
{

	public bool destroyWhenActivated = false;
	public bool onlyAffectPlayers = false;

	public int healthChange = -1;

	public float modifyDelay = 1.5f;
	private float lastModify = 0f;

	//This will create a dialog window asking for which dialog to add
	private void Reset()
	{
		Utils.Collider2DDialogWindow(this.gameObject, true);
	}

	// This function gets called everytime this object collides with another
	private void OnCollisionEnter2D(Collision2D collisionData)
	{
		OnTriggerEnter2D(collisionData.collider);
	}

	private void OnTriggerEnter2D(Collider2D colliderData)
	{
		if (onlyAffectPlayers && !colliderData.gameObject.tag.StartsWith("Player"))
			return;

		HealthSystemAttribute healthScript = colliderData.gameObject.GetComponent<HealthSystemAttribute>();
		if(healthScript != null)
		{
			if (modifyDelay > -1 && (Time.realtimeSinceStartup - lastModify) < modifyDelay)
				return;

			// subtract health from the player
			healthScript.ModifyHealth(healthChange);
			lastModify = Time.realtimeSinceStartup;

			if (destroyWhenActivated) Destroy(this.gameObject);
		}
	}
}
