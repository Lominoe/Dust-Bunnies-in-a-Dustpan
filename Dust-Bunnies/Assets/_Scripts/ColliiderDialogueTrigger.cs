using UnityEngine;

public class ColliiderDialogueTrigger : MonoBehaviour
{
    public string Dialogue = null;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other) {
        if (!hasTriggered) {
            DialogueManager.Instance.RunDialogue(Dialogue);
        }
    }
}
