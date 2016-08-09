using UnityEngine;
using System.Collections;

public enum Direction {
	Right, Left, Top, Bottom
}

public enum HitBoxType {
	Damage, Attack, Guard
}

public enum PlayerAction {
	Special,
	Normal,
	SmashLeft,
	SmashRight,
	Guard
}