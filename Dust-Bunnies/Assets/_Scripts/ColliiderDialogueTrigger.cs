using UnityEngine;

public class ColliiderDialogueTrigger : MonoBehaviour
{
    public string Dialogue = null;

    private void OnTriggerEnter(Collider other) {
        DialogueManager.Instance.RunDialogue(Dialogue);
    }
}
