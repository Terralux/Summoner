using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentTree  {

	public int talentPoints;
	public List<BaseSkill> listOfTalents;

	private Dictionary<BaseSkill, bool> tree;

	public TalentTree() {
		tree = new Dictionary<BaseSkill, bool> ();
		talentPoints = 0;
	}

	public void InitTalentTree(List<BaseSkill> talents) {
		foreach (BaseSkill t in talents) {
			tree.Add (t, false);
		}
	}

	public void LearnTalent(BaseSkill talent) {
		if (tree.ContainsKey (talent)) {
			if (talentPoints > 0) {
				tree [talent] = true;
				AdjustTalentPoints (-1);
			} else {
				Debug.LogWarning ("Hey you, you don't have any talent points!");
			}
		} else {
			Debug.LogWarning ("There should always be a talent of this type");
		}
	}

	public void UnlearnTalent(BaseSkill talent) {
		if (tree.ContainsKey (talent)) {
			if (tree [talent] == true) {
				tree.Remove (talent);
				AdjustTalentPoints (+1);
			}
		} else {
			Debug.LogWarning ("There should always be a talent of this type");
		}
	}

	public void ResetTalents() {
		int temp = 0;
		// Check if talent in tree has been learned, if so add 1 to temp
		foreach (BaseSkill t in listOfTalents) {
			if (tree [t] == true) {
				temp++;
			}
		}
		// Empty the tree, initiliaze a new one and refund all points accumulated in temp
		tree.Clear();
		InitTalentTree (listOfTalents);
		AdjustTalentPoints (temp);
	}

	public void AdjustTalentPoints(int value) {
		talentPoints += value;
	}

}