using UnityEngine;
using System.Collections;
using Vuforia;

public class CatHandler : 	MonoBehaviour,
							ITrackableEventHandler,
							IVirtualButtonEventHandler
{
	public GameObject cat;
	public GameObject releaseBtn;
	public GameObject btnPlane;
	public GameObject btnText;
	public Material greenMat, redMat;

	#region PRIVATE_MEMBER_VARIABLES

	private TrackableBehaviour mTrackableBehaviour;
	private VirtualButtonBehaviour btnBehaviour;
	private bool isReleased;
	private bool btnPressed;

	#endregion // PRIVATE_MEMBER_VARIABLES



	#region UNTIY_MONOBEHAVIOUR_METHODS

	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		btnBehaviour = releaseBtn.GetComponent<VirtualButtonBehaviour> ();
		btnBehaviour.RegisterEventHandler (this);

		isReleased = false;
	}
		
	#endregion // UNTIY_MONOBEHAVIOUR_METHODS



	#region PUBLIC_METHODS

	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}

	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) {
		MeshRenderer planeRenderer = btnPlane.GetComponent<MeshRenderer> ();
		TextMesh textMesh = btnText.GetComponent<TextMesh> ();

		if (isReleased) {
			planeRenderer.material = greenMat;
			textMesh.text = "Release";
			isReleased = false;
		} else {
			planeRenderer.material = redMat;
			textMesh.text = "Recall";
			isReleased = true;
		}
	}

	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) {
	}

	#endregion // PUBLIC_METHODS



	#region PRIVATE_METHODS


	private void OnTrackingFound()
	{
		showObj (cat, true);
		showObj (releaseBtn, true);
	}
		
	private void OnTrackingLost()
	{
		if (!isReleased) {
			showObj (cat, false);
		}

		showObj (releaseBtn, false);
	}

	private void showObj(GameObject obj, bool show) {
		Renderer[] rendererComponents = obj.GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = obj.GetComponentsInChildren<Collider>(true);

		// Enable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = show;
		}

		// Enable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = show;
		}
	}

	#endregion // PRIVATE_METHODS
}
