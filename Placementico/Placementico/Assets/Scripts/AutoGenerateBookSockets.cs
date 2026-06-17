using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class AutoGenerateBookSockets : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform booksParent;
    [SerializeField] private Transform socketStartPoint;

    [Header("Layout")]
    [SerializeField] private Vector3 socketDirection = Vector3.right;
    [SerializeField] private float extraGap = 0.01f;

    [Header("Book Standing Rotation")]
    [SerializeField] private Vector3 socketRotationOffset = new Vector3(0f, 0f, 90f);

    [Header("Socket Settings")]
    [SerializeField] private InteractionLayerMask bookInteractionLayer;
    [SerializeField] private InteractionLayerMask placedBookInteractionLayer;
    [SerializeField] private Vector3 socketTriggerSize = new Vector3(0.12f, 0.35f, 0.22f);

    private void Start()
    {
        GenerateSockets();
    }

    private void GenerateSockets()
    {
        int socketCount = booksParent.childCount;

        Vector3 worldDirection =
            socketStartPoint.TransformDirection(socketDirection.normalized);

        Quaternion socketRotation =
            socketStartPoint.rotation * Quaternion.Euler(socketRotationOffset);

        float spacing = GetBookSpacing(worldDirection, socketRotation);

        for (int i = 0; i < socketCount; i++)
        {
            GameObject socketObj = new GameObject($"BookSocket_{i + 1}");
            socketObj.transform.SetParent(transform);

            socketObj.transform.position =
                socketStartPoint.position + worldDirection * spacing * i;

            socketObj.transform.rotation = socketRotation;

            BoxCollider trigger = socketObj.AddComponent<BoxCollider>();
            trigger.isTrigger = true;
            trigger.size = socketTriggerSize;

            BookSocketInteractor socket = socketObj.AddComponent<BookSocketInteractor>();
            socket.interactionLayers = bookInteractionLayer;
            socket.unplacedBookLayer = bookInteractionLayer;
            socket.placedBookLayer = placedBookInteractionLayer;
        }
    }

    private float GetBookSpacing(Vector3 worldDirection, Quaternion socketRotation)
    {
        if (booksParent.childCount == 0)
            return 0.15f;

        Transform book = booksParent.GetChild(0);

        Vector3 bookSize = book.localScale;

        Vector3 localDirection =
            Quaternion.Inverse(socketRotation) * worldDirection.normalized;

        localDirection = new Vector3(
            Mathf.Abs(localDirection.x),
            Mathf.Abs(localDirection.y),
            Mathf.Abs(localDirection.z)
        );

        float sizeAlongDirection =
            bookSize.x * localDirection.x +
            bookSize.y * localDirection.y +
            bookSize.z * localDirection.z;

        return sizeAlongDirection + extraGap;
    }
}