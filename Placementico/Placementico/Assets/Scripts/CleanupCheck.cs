using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CleanupCheck : MonoBehaviour
{
    [SerializeField] private int totalObjects;

    private readonly HashSet<GameObject> cleanedObjects = new();

    public void OnObjectPlaced(SelectEnterEventArgs args)
    {
        GameObject obj = args.interactableObject.transform.gameObject;

        cleanedObjects.Add(obj);

        Debug.Log($"Cleaned: {cleanedObjects.Count}/{totalObjects}");

        if (cleanedObjects.Count >= totalObjects)
        {
            Debug.Log("Room cleaned! You win.");
        }
    }

    public void OnObjectRemoved(SelectExitEventArgs args)
    {
        GameObject obj = args.interactableObject.transform.gameObject;

        cleanedObjects.Remove(obj);

        Debug.Log($"Cleaned: {cleanedObjects.Count}/{totalObjects}");
    }
}
