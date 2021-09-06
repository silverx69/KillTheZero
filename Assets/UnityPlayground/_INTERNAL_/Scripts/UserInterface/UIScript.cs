using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[AddComponentMenu("")]
public class UIScript : MonoBehaviour {

	[Header("Configuration")]
	public GameType gameType = GameType.Score;

	public int scoreForZero = 2;
	public int scoreToWin = 5;

	[Header("References (don't touch)")]
	//Right is used for the score in P1 games
	public Text[] numberLabels = new Text[3];
	public Text rightTopLabel, leftTopLabel, rightBottomLabel;
	public Text winLabel;
	public GameObject zeroPrefab;
	public GameObject statsPanel, zeroPanel, gameOverPanel, winPanel;
	public Transform inventory;
	public GameObject resourceItemPrefab;

	// Internal variables to keep track of score, health, and resources, win state
	public int[] scores = new int[2];
	public int[] playersHealth = new int[2];

	private int lastZero = 0;

	//holds a reference to all the resources collected, and to their UI
	private Dictionary<int, ResourceStruct> resourcesDict = new Dictionary<int, ResourceStruct>();
	private bool gameOver = false; //this gets changed when the game is won OR lost

    private void Start() {
		lastZero = scoreForZero;
    }

    //version of the one below with one parameter to be able to connect UnityEvents
    public void AddOnePoint(int playerNumber) {
		AddPoints(playerNumber, 1);
	}

	public void AddPoints(int playerNumber, int amount = 1) {
		scores[playerNumber] += amount;
		numberLabels[1].text = scores[playerNumber].ToString(); //with one player, the score is on the right

		if (gameType == GameType.Score && scores[playerNumber] >= scoreToWin)
			GameWon(playerNumber);
		/*
		if (scores[playerNumber] >= lastZero) {
			var cp = Camera.main.transform.position;
			SpawnZero(new Vector2(cp.x, cp.y - 1));
			lastZero += scoreForZero;
        }
		*/
	}

	//currently unused by other Playground scripts
	public void RemoveOnePoint(int playerNumber) {
		scores[playerNumber]--;
		numberLabels[1].text = scores[playerNumber].ToString(); //with one player, the score is on the right
	}

	public void SpawnZero(Vector2 location) {

		GameObject newObject = Instantiate<GameObject>(zeroPrefab);
		newObject.transform.position = location;

		ZeroSpawned();
	}

	public void ZeroSpawned() {
		if (!zeroPanel.activeInHierarchy)
			zeroPanel.SetActive(true);
    }

	public void GameWon(int playerNumber) {
		// only set game over UI if game is not over
		if (!gameOver) {
			gameOver = true;
			winLabel.text = string.Format("Player {0} wins!", ++playerNumber);
			statsPanel.SetActive(false);
			winPanel.SetActive(true);
		}
	}

	public void GameOver(int playerNumber) {
		// only set game over UI if game is not over
		if (!gameOver) {
			gameOver = true;
			statsPanel.SetActive(false);
			gameOverPanel.SetActive(true);
		}
	}

	public void SetHealth(int amount, int playerNumber) {
		playersHealth[playerNumber] = amount;
		numberLabels[playerNumber].text = playersHealth[playerNumber].ToString();
	}

	public void ChangeHealth(int change, int playerNumber) {
		SetHealth(playersHealth[playerNumber] + change, playerNumber);

		if (gameType != GameType.Endless && playersHealth[playerNumber] <= 0) {
			GameOver(playerNumber);
		}

	}

	//Adds a resource to the dictionary, and to the UI
	public void AddResource(int resourceType, int pickedUpAmount, Sprite graphics) {
		AddResource(resourceType, pickedUpAmount, -1, graphics);
	}

	//Adds a resource to the dictionary, and to the UI
	public void AddResource(int resourceType, int pickedUpAmount, int maxAmount, Sprite graphics) {
		int newAmount = pickedUpAmount;
		if (resourcesDict.ContainsKey(resourceType)) {
			//update the dictionary key
			newAmount += resourcesDict[resourceType].amount;
		}
		else {
			//create the UIItemScript and display the icon
			UIItemScript newUIItem = Instantiate<GameObject>(resourceItemPrefab).GetComponent<UIItemScript>();
			newUIItem.transform.SetParent(inventory, false);

			resourcesDict.Add(resourceType, new ResourceStruct(newAmount, newUIItem));
			resourcesDict[resourceType].UIItem.DisplayIcon(graphics);
		}

		if (maxAmount > -1 && newAmount >= maxAmount) 
			newAmount = maxAmount;

		resourcesDict[resourceType].UIItem.ShowNumber(newAmount);
		resourcesDict[resourceType].amount = newAmount;
	}

	//checks if a certain resource is in the inventory, in the needed quantity
	public bool CheckIfHasResources(int resourceType, int amountNeeded = 1) {
		if (resourcesDict.ContainsKey(resourceType)) {
			if (resourcesDict[resourceType].amount >= amountNeeded) {
				return true;
			}
			else {
				//not enough
				return false;
			}
		}
		else {
			//resource not present
			return false;
		}
	}

	//to use only before checking that the resource is in the dictionary
	public void ConsumeResource(int resourceType, int amountNeeded = 1) {
		resourcesDict[resourceType].amount -= amountNeeded;
		resourcesDict[resourceType].UIItem.ShowNumber(resourcesDict[resourceType].amount);
	}

	public enum GameType {
		Score = 0,
		Life,
		Endless
	}
}



//just a virtual representation of the resources for the private dictionary
public class ResourceStruct {
	public int amount;
	public UIItemScript UIItem;

	public ResourceStruct(int a, UIItemScript uiRef) {
		amount = a;
		UIItem = uiRef;
	}
}