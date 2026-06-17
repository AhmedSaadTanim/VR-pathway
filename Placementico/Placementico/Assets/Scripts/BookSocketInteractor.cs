using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BookSocketInteractor : XRSocketInteractor
{
    public InteractionLayerMask unplacedBookLayer;
    public InteractionLayerMask placedBookLayer;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactableObject is XRGrabInteractable grab)
            grab.interactionLayers = placedBookLayer;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactableObject is XRGrabInteractable grab)
            grab.interactionLayers = unplacedBookLayer;

        base.OnSelectExited(args);
    }
}