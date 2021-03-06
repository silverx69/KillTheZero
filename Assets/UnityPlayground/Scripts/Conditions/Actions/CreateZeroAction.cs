using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Actions/Create Zero")]
public class CreateZeroAction : Action {
	public GameObject userInterface;
	public GameObject prefabToCreate;
	public Vector2 newPosition;
	public bool relativeToThisObject;

	// Creates a new GameObject
	public override bool ExecuteAction(GameObject dataObject) {

		if (prefabToCreate != null) {
			//create the new object by copying the prefab
			GameObject newObject = Instantiate<GameObject>(prefabToCreate);

			//is the position relative or absolute?
			Vector2 finalPosition = newPosition;
			if (relativeToThisObject) {
				finalPosition = (Vector2)transform.position + newPosition;
			}

			//let's place it in the desired position!
			newObject.transform.position = finalPosition;

			if (userInterface != null) {
				var uiscript = userInterface.GetComponent<UIScript>();
				if (uiscript != null) uiscript.ZeroSpawned();
			}

			return true;
		}
		else {
			Debug.LogWarning("There is no Prefab assigned to this CreateObjectAction, so a new object can't be created.");
			return false;
		}
	}
}