using UnityEngine;

[CreateAssetMenu(fileName = "SnapshotLockRegistry", menuName = "Scriptable Objects/Lock")]
public class SnapshotRequirement : ScriptableObject
{
    public int snapshotNumber;
    public UnlockCondition condition;
    public string conditionID;
}
