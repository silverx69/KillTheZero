using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableScript : MonoBehaviour
{
	public bool lootMultiple = false;

	private HealthSystemAttribute healthSystem;

	private void Start() {
		healthSystem = gameObject.GetComponent<HealthSystemAttribute>();
	}

	private void DoLootRoll() {

        var results = new List<LootScript>();
        GetComponents(results);

        for (int i = 0; i < results.Count; i++) {
            float roll = Random.value; // 0..1
            if (roll <= results[i].chance) {
                //spawn this loot prefab
                //create the new object by copying the prefab
                 GameObject newObject = Instantiate<GameObject>(results[i].prefabToSpawn);
                //let's place it in the desired position!
                newObject.transform.position = transform.position;
                if (!lootMultiple) break;
            }
        }
    }

	//This will create a dialog window asking for which dialog to add
	private void Reset() {
		Utils.Collider2DDialogWindow(this.gameObject, false);
	}

	//duplication of the following function to accomodate both trigger and non-trigger Colliders
	private void OnCollisionEnter2D(Collision2D collisionData) {
		OnTriggerEnter2D(collisionData.collider);
	}

	// This function gets called everytime this object collides with another trigger
	private void OnTriggerEnter2D(Collider2D collisionData) {
		// is the other object a Bullet?
		if (collisionData.gameObject.CompareTag("Bullet")) {
			BulletAttribute b = collisionData.gameObject.GetComponent<BulletAttribute>();
			if (b != null) {
				if (healthSystem != null) {
					int health = healthSystem.health;
					if (health <= 0) DoLootRoll();
				}
			}
			else Debug.Log("Use a BulletAttribute on one of the objects involved in the collision if you want one of the players to receive loot for destroying the target.");
		}
	}
}
