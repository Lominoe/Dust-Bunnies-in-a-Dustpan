using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SnapshotLockRegistry", menuName = "Scriptable Objects/SnapshotLockRegistry")]
public class SnapshotLockRegistry : ScriptableObject
{
    [SerializeField] private List<SnapshotRequirement> requirements = new();

    // runtime state: which conditionIDs have been satisfied
    private readonly HashSet<string> metConditions = new();

    public void ReportConditionMet(UnlockCondition condition, string id) {
        metConditions.Add(MakeKey(condition, id));
    }

    public bool IsUnlocked(int snapshotNumber) {
        foreach (var req in requirements) {         // TODO: this can be more efficient I think?
            if (req.snapshotNumber == snapshotNumber) {
                if (!metConditions.Contains(MakeKey(req.condition, req.conditionID)))
                    return false;
            }
        }
        return true;
    }

    private string MakeKey(UnlockCondition c, string id) => $"{c}:{id}";

    public void Reset() => metConditions.Clear();
}
