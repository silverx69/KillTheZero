using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Gameplay/Object Creator Area")]
[RequireComponent(typeof(BoxCollider2D))]
public class ObjectCreatorArea : MonoBehaviour
{
	[Header("Object creation")]

	// The object to spawn
	// WARNING: take if from the Project panel, NOT the Scene/Hierarchy!
	public GameObject[] prefabsToSpawn;

	[Header("Other options")]

	// Configure the spawning pattern
	public float spawnInterval = 1;

	public bool randomInterval = false;
	public Vector2 randomRange = new Vector2(0, 1);

	private bool skipFirst = true;
	private BoxCollider2D boxCollider2D;

	void Start ()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();

		if (randomInterval)
			spawnInterval = Random.Range(randomRange.x, randomRange.y);

		StartCoroutine(SpawnObject());
	}
	
	// This will spawn an object, and then wait some time, then spawn another...
	IEnumerator SpawnObject ()
	{
		while(true)
		{
			if (!skipFirst) {
				// Create some random numbers
				float randomX = Random.Range(-boxCollider2D.size.x, boxCollider2D.size.x) * .5f;
				float randomY = Random.Range(-boxCollider2D.size.y, boxCollider2D.size.y) * .5f;

				// Generate the new object
				GameObject newObject = Instantiate<GameObject>(prefabsToSpawn[Mathf.RoundToInt(Random.Range(0, prefabsToSpawn.Length))]);
				newObject.transform.position = new Vector2(randomX + this.transform.position.x, randomY + this.transform.position.y);

				if (randomInterval) 
					spawnInterval = Random.Range(randomRange.x, randomRange.y);
			}
			else skipFirst = false;
			// Wait for some time before spawning another object
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
