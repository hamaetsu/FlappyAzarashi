using UnityEngine;
using System.Collections;

public class ClearTrigger : MonoBehaviour 
{
	GameObject gameController;

	void Start() 
	{
		// ゲーム開始時にGameControllerをFindしておく
		gameController = GameObject.FindWithTag("GameController");
	}

	// トリガーからExitしたらクリアとみなす
	void OnTriggerExit2D(Collider2D other)
	{
		gameController.SendMessage("IncreaseScore");
	}
}
