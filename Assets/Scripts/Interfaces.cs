using UnityEngine;
using System.Collections;

public interface ICharacterController {
	void Jump ();
	void Special ();
	void SpecialLeft ();
	void SpecialRight ();
	void SpecialDown ();
	void SpecialUp ();
	void Normal ();
	void SmashLeft ();
	void SmashRight();
	void SmashDown ();
	void SmashUp ();
}