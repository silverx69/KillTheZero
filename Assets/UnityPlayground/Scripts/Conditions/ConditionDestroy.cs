using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Playground/Conditions/Condition Destroy")]
[RequireComponent(typeof(Collider2D))]
public class ConditionDestroy : ConditionBase {

	private void OnDestroy() {
		if (gameObject.CompareTag(filterTag) || !filterByTag)
			ExecuteAllActions(gameObject);
	}
}
