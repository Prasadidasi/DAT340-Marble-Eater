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
    public bool isWorldSpawned = false;
    public bool scan = false;
   // [SerializeField] private string key;
    // Start is called before the first frame update
    void Awake() => _trackedImageManager = GetComponent<ARTrackedImageManager>();

    private void Start()
    {
       // Debug.Log(isWorldSpawned);
    }

    private void OnEnable() => _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    

    private void OnDisable() => _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    

    // Update is called once per frame

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var addedImage in eventArgs.added)
        {
            //Debug.Log("Image Added");
            if (scan == true)
                HandleImageAdding(addedImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (scan == true)
                HandleImageUpdating(updatedImage);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            HandleImageRemoving(removedImage);
        }
    }

    private void HandleImageAdding(ARTrackedImage addedImage)
    {

        scan = false;
      //  Debug.Log("Image pos: " + addedImage.transform.localPosition);
        Transform spawnLocation = addedImage.transform;
       
        spawnLocation.localPosition += new Vector3(0, 0.2f, 0);
       // Debug.Log("Box pos: "+ spawnLocation.transform.localPosition);
        _instantiatedPrefab = Instantiate(prefab, spawnLocation);
        _instantiatedPrefab.transform.parent = GetComponent<ARSessionOrigin>().trackablesParent;
        //_instantiatedParticleSystem = Instantiate(particleSystem, addedImage.transform);
        //_instantiatedParticleSystem.transform.parent = GetComponent<ARSessionOrigin>().trackablesParent;
        isWorldSpawned = true;
        //Debug.Log(isWorldSpawned);
    }

    private void HandleImageUpdating(ARTrackedImage updatedImage)
    {
        HandleImageAdding(updatedImage);
    }

    private void HandleImageRemoving(ARTrackedImage removedImage)
    {
        
    }

    public void enableScan()
    {
        scan = true;
      //  Debug.Log(scan);
    }

    public void disableScan()
    {
        scan = false;
     //   Debug.Log(scan);
    }
}
