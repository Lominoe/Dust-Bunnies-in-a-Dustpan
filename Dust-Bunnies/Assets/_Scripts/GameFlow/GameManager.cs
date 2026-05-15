using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private SnapshotLockRegistry lockRegistery;

    public static event System.Action OnLoadNextSnapshot;
    public static event System.Action OnLoadPreviousSnapshot;
    public static event System.Action OnRestartGame;
    public static event System.Action OnLevelIsLocked;

    public static Vector3 playerCoords;       // used for when the player teleports to a new scene and coords need to be preserved
    public static Quaternion playerRotation;  // ^
    public static bool hasStoredData = false;

    private static int currSnapshotNumber = 0;
    public static int CurrentSnapshotNumber => currSnapshotNumber;
     
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private PlayerController player;
    public static PlayerController Player {
        get => Instance.player;
        set { 
            if (Instance.player == null && value) {
                Instance.player = value;
                // can add an event here that notifies systems that the player has been assigned
            }
        }
    }

    /// <summary>
    /// Store position and rotation based on the players position
    /// relative to the room to be used when they next spawned in
    /// </summary>
    public static void StorePlayerPosition(Vector3 pos, Quaternion rot) {
        playerCoords = pos;
        playerRotation = rot;
        hasStoredData = true;
    }

    public static void ChangeSnapshot(int direction) {
        int target = currSnapshotNumber + direction;

        if (!CheckSnapshotRequirements(target)) {
            OnLevelIsLocked?.Invoke();
            return;
        }

        currSnapshotNumber = target;

        if (direction > 0)
            OnLoadNextSnapshot?.Invoke();
        else
            OnLoadPreviousSnapshot?.Invoke();
    }

    /// <summary>
    /// Parameter is the snapshot the player is trying to switch to
    /// returns true if you can switch and false if requirements are missing
    /// </summary>
    public static bool CheckSnapshotRequirements(int snapshotNumber) {
        if (snapshotNumber <= 0) return false;      // no going back to main menu
        if (snapshotNumber == 1) return true;       // only the mirror can switch to snapshot 1
        return Instance.lockRegistery.IsUnlocked(snapshotNumber);
    }

    /// <summary>
    /// Called when a level lock requirement is completed
    /// </summary>
    public static void ReportUnlock(UnlockCondition condition, string id) {
        Instance.lockRegistery.ReportConditionMet(condition, id);
        Debug.Log("Unlock Condition: " + condition + " | Id: " +  id);
    }

    public static void RestartGame() {
        currSnapshotNumber = 0;
        hasStoredData = false;
        Instance.lockRegistery.Reset();
        OnRestartGame?.Invoke();
    }
}
