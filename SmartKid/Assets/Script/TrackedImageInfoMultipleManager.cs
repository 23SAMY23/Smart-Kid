using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoMultipleManager : MonoBehaviour
{
    
    public GameObject goARObject;

    public Button urdu;
    public Button chinesse;
    public Button english;
    
    public GameObject texttex;
    [SerializeField]
    public GameObject[] arObjectsToPlace;
    public AudioClip[] urdulist;
    public AudioSource urduSource;

    public AudioClip[] englishlist;
    public AudioSource englishSource;

    public AudioClip[] chineselistlist;
    public AudioSource chinaSource;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(10f, 10f, 10f);

    private ARTrackedImageManager m_TrackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

        // setup all game objects in dictionary
        foreach (GameObject arObject in arObjectsToPlace)
        {
            //instiate the object prefabs
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;//get the name of the object 
            arObjects.Add(arObject.name, newARObject);//add the object 
            newARObject.SetActive(false);//make it not visible
           
        }
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; // track image using camera
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged; 
    }


    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage); 
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            arObjects[trackedImage.name].SetActive(false);
        }
    }// track image we currently have

    private void UpdateARImage(ARTrackedImage trackedImage)
    {

        // Assign and Place Game Object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);

        
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        if (arObjectsToPlace != null)
        {
            goARObject = arObjects[name];
            goARObject.SetActive(true);
            goARObject.transform.position = newPosition;
           goARObject.transform.localScale = scaleFactor;
            

            foreach (GameObject go in arObjects.Values)
            {
                Debug.Log($"Go in arObjects.Values: {go.name}");
                if (go.name != name)
                {
                    go.SetActive(false);
                    
                }
            }
            
        }
    }//to make  an object apear on the track image acoording to the name of the image
    public void urdubutton(string clipToPlay)
    {
        
            
    
        clipToPlay = goARObject.name;
        
        foreach (AudioClip clip in urdulist)
        {
            if (clip.name != clipToPlay)
            {

            }
            if (clip.name == clipToPlay)
            {
                urduSource.PlayOneShot(clip);
            }
            
        }
    }//play audio according to the image/object name
    public void englishbutton(string clipToPlay)
    {
        clipToPlay = goARObject.name;
        foreach (AudioClip clip in englishlist)
        {
            if (clip.name != clipToPlay)
            {

            }
            if (clip.name == clipToPlay)
                englishSource.PlayOneShot(clip);
            
        }

    }//play audio according to the image/object name
    public void chinabutton(string clipToPlay)
    {
        clipToPlay = goARObject.name;
        foreach (AudioClip clip in chineselistlist)
        {
            if (clip.name != clipToPlay)
            {

            }
            if (clip.name == clipToPlay)
                chinaSource.PlayOneShot(clip);
           
        }

    }//play audio according to the image/object name
}
