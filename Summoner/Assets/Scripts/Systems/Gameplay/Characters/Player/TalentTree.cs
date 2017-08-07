using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentTree
{

	public int talentPoints;
	public List<BaseSkill> listOfTalents = new List<BaseSkill> ();
	// cooldown categories and their values
	private float shortCD = 5f;
	private float mediumCD = 10f;
	private float longCD = 20f;
	private int totalTalents = 17;

	private Dictionary<BaseSkill, bool> tree;

	public TalentTree ()
	{
		SetupListOfTalents ();
		talentPoints = 0;
		tree = new Dictionary<BaseSkill, bool> ();
		InitTalentTree ();
	}

	private void InitTalentTree ()
	{
		foreach (BaseSkill t in listOfTalents) {
			tree.Add (t, false);
		}
	}

	//!!!!!! Add all talents a player can learn here, HARD-CODED!!!!!!!!!!
	private void SetupListOfTalents ()
	{
		listOfTalents.Add (new AoeHeal ("AOE HEAL", mediumCD, 0, new Sprite ()));
		listOfTalents.Add (new HealTrap ("HEAL TRAP", longCD, 1, new Sprite ()));
		listOfTalents.Add (new AoeHot ("AOE HOT", longCD, 2, new Sprite ()));
		listOfTalents.Add (new ForcePush ("FORCE PUSH", shortCD, 3, new Sprite ()));
		listOfTalents.Add (new ForcePull ("FORCE PULL", shortCD, 4, new Sprite ()));
		listOfTalents.Add (new BashStrike ("BASH STRIKE", shortCD, 5, new Sprite ()));
		listOfTalents.Add (new Counter ("COUNTER", shortCD, 6, new Sprite ()));
		listOfTalents.Add (new Decoy ("DECOY", longCD, 7, new Sprite ()));
		listOfTalents.Add (new MagnetTrap ("MAGNET TRAP", longCD, 8, new Sprite ()));
		listOfTalents.Add (new PoisonTrap ("POISON TRAP", longCD, 9, new Sprite ()));
		listOfTalents.Add (new Block ("BLOCK", shortCD, 10, new Sprite ()));
		listOfTalents.Add (new Pierce ("PIERCE", mediumCD, 11, new Sprite ()));
		listOfTalents.Add (new RaiseWall ("RAISE WALL", longCD, 12, new Sprite ()));
		listOfTalents.Add (new MeleeRevive ("MELEE REVIVE", longCD, 13, new Sprite ()));
		listOfTalents.Add (new CircularAttack ("CIRCULAR ATTACK", shortCD, 14, new Sprite ()));
		listOfTalents.Add (new Stun ("STUN", mediumCD, 15, new Sprite ()));
		listOfTalents.Add (new Provoke ("PROVOKE", mediumCD, 16, new Sprite ()));
	}

	private bool TalentExists(int skillIndex) {
		return (skillIndex >= 0 && skillIndex < totalTalents);
	}

	public void LearnTalent (int skillIndex)
	{
		if (!TalentExists(skillIndex)) {
			Debug.LogWarning ("Skill Index out of bounds");
			return;
		} else {

			if (talentPoints > 0) {
				tree [listOfTalents [skillIndex]] = true;
				AdjustTalentPoints (-1);
			} else {
				Debug.LogWarning ("Hey you, you don't have any talent points!");
			}
		}

	}

	public void UnlearnTalent (int skillIndex)
	{

		if (!TalentExists(skillIndex)) {
			Debug.LogWarning ("Skill Index out of bounds");
			return;
		} else {

			if (tree [listOfTalents [skillIndex]] == true) {
				tree.Remove (listOfTalents [skillIndex]);
				AdjustTalentPoints (+1);
			}
		}
	}

	public void ResetTalents ()
	{
		int temp = 0;
		// Check if talent in tree has been learned, if so add 1 to temp
		foreach (BaseSkill t in listOfTalents) {
			if (tree [t] == true) {
				temp++;
			}
		}
		// Empty the tree, initiliaze a new one and refund all points accumulated in temp
		tree.Clear ();
		InitTalentTree ();
		AdjustTalentPoints (temp);
	}

	public void AdjustTalentPoints (int value)
	{
		talentPoints += value;
	}

	public BaseSkill GetTalentDetails(int skillIndex) {
		return listOfTalents [skillIndex];
	}

	public bool HasLearnedTalent(int skillIndex) {
		return tree [listOfTalents [skillIndex]];
	}

}