using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPrefab : MonoBehaviour
{
	public GameObject userInterface;
	public float respawnDelay = 150f;
    public bool spawnOnStart = true;
    public GameObject prefabToSpawn;
	public Vector2 newPosition;
	public Vector2 newScale = new Vector2(1f, 1f);
	public bool relativeToThisObject;

	private float lastDied = 0f;
	private float lastCheck = 0f;

	// Start is called before the first frame update
	void Start() {
		lastCheck = Time.realtimeSinceStartup;
        if (spawnOnStart)
			DoSpawn();
    }

    // Update is called once per frame
    void Update() {
		float rt = Time.realtimeSinceStartup;
		if (rt - lastCheck >= 5f) {
			lastCheck = rt;
			
			var allChildren = GetComponentsInChildren<Transform>();
			if (allChildren.Length > 0)
				return;
			else {
				if (lastDied > 0f) {
					if (rt - lastDied >= respawnDelay)
						DoSpawn();
				}
				else {
					lastDied = rt;
				}
			}
		}
	}

    private void DoSpawn() {
		if (prefabToSpawn != null) {
			lastDied  = 0f;
			//create the new object by copying the prefab
			GameObject newObject = Instantiate<GameObject>(prefabToSpawn, transform);

			var zeroAction = newObject.GetComponent<CreateZeroAction>();
			if (zeroAction != null) 
				zeroAction.userInterface = userInterface;
			
			//is the position relative or absolute?
			Vector2 finalPosition = newPosition;

			if (relativeToThisObject)
				finalPosition = (Vector2)transform.position + newPosition;

			//let's place it in the desired position!
			newObject.transform.position = finalPosition;
			newObject.transform.localScale = newScale;
		}
		else {
			Debug.LogWarning("There is no Prefab assigned to this CreateObjectAction, so a new object can't be created.");
		}
	}
}
