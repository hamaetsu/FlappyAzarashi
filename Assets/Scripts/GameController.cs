using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{

	// ゲームステート
	enum State
	{
		Ready,
		Play,
		GameOver
	}

	State state;
	int score;

	public AzarashiController azarashi;
	public GameObject blocks;

	void Start () 
	{
		// 開始と同時にReadyステートに移行
		Ready();
	}

	void LateUpdate()
	{
		// ゲームのステートごとにイベントを監視
		switch (state)
		{
		case State.Ready:
			// タッチしたらゲームスタート
			if (Input.GetButtonDown("Fire1"))
			{
				GameStart();
			}
			break;

		case State.Play:
			// キャラクターが死亡したらゲームオーバー
			if (azarashi.IsDead())
			{
				GameOver();
			}
			break;
		case State.GameOver:
			// タッチしたらシーンをリロード
			if (Input.GetButtonDown("Fire1"))
			{
				Reload();
			}
			break;
		}
	}

	void Ready()
	{
		state = State.Ready;
		// 各オブジェクトを無効状態にする
		azarashi.SetSteerActive(false);
		blocks.SetActive(false);
	}

	void GameStart()
	{
		state = State.Play;

		// 各オブジェクトを有効にする
		azarashi.SetSteerActive(true);
		blocks.SetActive(true);

		// 最初の入力だけゲームコントローラーから渡す
		azarashi.Flap();
	}

	void GameOver()
	{
		state = State.GameOver;

		// シーン中のすべてのScrollObjectコンポーネントを探しだす
		ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();

		// 全ScrollObjectのスクロール処理を無効にする
		foreach (ScrollObject so in scrollObjects)
		{
			so.enabled = false;
		}
	}

	void Reload()
	{
		// 現在読み込んでいるシーンを再読み込み
		Application.LoadLevel(Application.loadedLevel);
	}

	public void IncreaseScore()
	{
		score++;
	}

}
