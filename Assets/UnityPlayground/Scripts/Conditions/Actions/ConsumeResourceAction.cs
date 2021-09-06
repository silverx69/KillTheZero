using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[AddComponentMenu("Playground/Actions/Consume Resource")]
public class ConsumeResourceAction : Action
{
	[Header("Resource")]

	public int checkFor = 0;
	public int amountNeeded = 1;
	public int givePoints = 0;
	public bool destroyWhenActivated = false;

	private UIScript userInterface;


	private void Start()
	{
		// Find the UI in the scene and store a reference for later use
		userInterface = GameObject.FindObjectOfType<UIScript>();
	}

	public override bool ExecuteAction(GameObject dataObject)
	{
		if(userInterface != null)
		{
			bool hasEnoughResource = userInterface.CheckIfHasResources(checkFor, amountNeeded);

			if(hasEnoughResource)
			{
				//consume the resource and update the UI
				userInterface.ConsumeResource(checkFor, amountNeeded);
				
				if (givePoints > 0) 
					userInterface.AddPoints(1, givePoints);

				if (destroyWhenActivated) 
					Destroy(gameObject);
			}

			return true;
		}
		else
		{
			Debug.LogWarning("User Interface prefab has not been found in the scene. The action can't execute!");
			return false;
		}
	}



}
