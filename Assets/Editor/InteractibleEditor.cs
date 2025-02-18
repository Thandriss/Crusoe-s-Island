using UnityEditor;
[CustomEditor(typeof(Interactable), true)]
public class InteractibleEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable) target;
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMes = EditorGUILayout.TextField("Prompt mess", interactable.promptMes);
            EditorGUILayout.HelpBox("EventOnlyInteractable can ONLY use UnityEvents", MessageType.Info);
            if (interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        base.OnInspectorGUI();
        if (interactable.useEvents)
        {
            if (interactable.GetComponent<Interactable>() == null)
            {
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            if (interactable.GetComponent<InteractionEvent>() != null)
            {
                DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }
    }
}
