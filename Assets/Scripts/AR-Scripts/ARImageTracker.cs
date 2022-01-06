using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTracker : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
   // [SerializeField] private GameObject particleSystem;
    [HideInInspector] public GameObject _instantiatedPrefab;
    //[HideInInspector]private GameObject _instantiatedParticleSystem;
    private ARTrackedImageManager _trackedImageManager;

    [HideInInspector] public WorldSetup WorldSetup;
    public bool isWorldSpawned { set; get; }
    private bool sizeUpdate;

    // Start is called before the first frame update
    private void Awake() => _trackedImageManager = GetComponent<ARTrackedImageManager>();

 
    private void OnEnable() => _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    

    private void OnDisable() => _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    

    // Update is called once per frame+

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var addedImage in eventArgs.added)
        {
            Debug.Log(WorldSetup.isScanning);
            Debug.Log("image added");
            if (WorldSetup.isScanning == true)
                HandleImageAdding(addedImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (WorldSetup.isScanning == true)
            {
                HandleImageUpdating(updatedImage);
                
                if(WorldSetup.isWorldSpawned == false)
                {
                    HandleImageAdding(updatedImage);
                }
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            HandleImageRemoving(removedImage);
        }
    }

    private void HandleImageAdding(ARTrackedImage addedImage)
    {        
        Debug.Log("Handling Image Adding");
        Transform spawnLocation = addedImage.transform;       
        spawnLocation.localPosition += new Vector3(0, 0.2f, 0);       
        _instantiatedPrefab = Instantiate(prefab, spawnLocation);
        _instantiatedPrefab.transform.parent = GetComponent<ARSessionOrigin>().trackablesParent;
       // _instantiatedParticleSystem = Instantiate(particleSystem, addedImage.transform);
        //_instantiatedParticleSystem.transform.parent = GetComponent<ARSessionOrigin>().trackablesParent;
        
        isWorldSpawned = true;
        WorldSetup.UpdateBoolChecks();
        Debug.Log("Image Added");
    }
    
    private void HandleImageUpdating(ARTrackedImage updatedImage)
    {
        WorldSetup.UpdateBoolChecks();
        Debug.Log("Update");
        //HandleImageAdding(updatedImage);
    }

    private void HandleImageRemoving(ARTrackedImage removedImage)
    {
        
    }
}
