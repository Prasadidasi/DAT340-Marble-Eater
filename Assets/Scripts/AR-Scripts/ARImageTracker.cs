using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTracker : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject particleSystem;
    private GameObject _instantiatedPrefab;
    private GameObject _instantiatedParticleSystem;
    private ARTrackedImageManager _trackedImageManager;
    
   // [SerializeField] private string key;
    // Start is called before the first frame update
    void Awake() => _trackedImageManager = GetComponent<ARTrackedImageManager>();
    

    private void OnEnable() => _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    

    private void OnDisable() => _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    

    // Update is called once per frame

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var addedImage in eventArgs.added)
        {
            HandleImageAdding(addedImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            HandleImageUpdating(updatedImage);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            HandleImageRemoving(removedImage);
        }
    }

    private void HandleImageAdding(ARTrackedImage addedImage)
    {
        _instantiatedPrefab = Instantiate(prefab, addedImage.transform);
        //_instantiatedPrefab.transform.parent = GetComponent<ARSessionOrigin>().trackablesParent;
        _instantiatedParticleSystem = Instantiate(particleSystem, addedImage.transform);
        _instantiatedParticleSystem.transform.parent = GetComponent<ARSessionOrigin>().trackablesParent;
    }

    private void HandleImageUpdating(ARTrackedImage updatedImage)
    {
        
    }

    private void HandleImageRemoving(ARTrackedImage removedImage)
    {
        
    }
}
