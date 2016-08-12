using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IMatchRules {
	void Increment ();
	void Decrement ();
	void Initialize ();
	bool CheckVictory ();
	PlayerController GetWinner ();
	int GetWinnerId ();
}

public enum HandicapMode {
	On, Off, Auto
}

public enum StageSelectionMode {
	On, Random, Ordered, Turns, Loser
}

[System.Serializable]
abstract public class MatchRules {
	abstract public string name { get; }
	public HandicapMode handicap;
	public float damageRatio = 1f;
	public StageSelectionMode stageMode;
}

[System.Serializable]
public class StockRules: MatchRules, IMatchRules {
	public int stockLimit = 3;
	public int stockMax = 99;
	public int stockMin = 1;

	private const int stockStep = 1;

	#region implemented abstract members of Rules

	public override string name {
		get { return "Stock"; }
	}

	#endregion

	#region IMatchRules implementation

	public void Increment() {
		stockLimit += stockStep;
		if (stockLimit > stockMax) {
			stockLimit = stockMax;
		}
	}

	public void Decrement() {
		stockLimit -= stockStep;
		if (stockLimit < stockMin) {
			stockLimit = stockMin;
		}	
	}

	public void Initialize () {
		throw new System.NotImplementedException ();
	}

	public bool CheckVictory () {
		var state = GameController.gameState.matchState;
		var countAlive = 0;
		for (var i = 0; i < state.stocks.Length; i++) {
			if (state.stocks [i] > 0) {
				countAlive += 1;
			}
		}

		if (countAlive == 0 || countAlive == 1) {
			return true;
		} else {
			return false;
		}
	}

	public PlayerController GetWinner() {
		return GameController.gameState.players [GetWinnerId ()];
	}

	public int GetWinnerId() {
		var state = GameController.gameState.matchState;
		var winnerId = 0;
		var previous = 0;
		for (var i = 0; i < state.stocks.Length; i++) {
			if (state.stocks [i] > previous) {
				winnerId = i;
				previous = state.stocks [i];
			}
		}
		return winnerId;
	}

	#endregion
}

[System.Serializable]
public class TimeRules: MatchRules, IMatchRules {
	public int timeLimit = 60;
	public int timeMax = 3600;
	public int timeMin = 30;

	private const int timeStep = 30;

	#region implemented abstract members of Rules
	public override string name {
		get {
			throw new System.NotImplementedException ();
		}
	}
	#endregion
	
	#region IMatchRules implementation
	public void Increment () {
		timeLimit += timeStep;
		if (timeLimit > timeMax) {
			timeLimit = timeMax;
		}
	}

	public void Decrement () {
		timeLimit -= timeStep;
		if (timeLimit < timeMin) {
			timeLimit = timeMin;
		}
	}

	public void Initialize () {
		throw new System.NotImplementedException ();
	}
	public bool CheckVictory () {
		throw new System.NotImplementedException ();
	}

	public PlayerController GetWinner () {
		throw new System.NotImplementedException ();
	}

	public int GetWinnerId () {
		throw new System.NotImplementedException ();
	}
	#endregion
}
	