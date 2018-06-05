using System;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

/// <summary>
/// This MonoBehaviour implements the Cloud Reco Event handling for this sample.
/// It registers itself at the CloudRecoBehaviour and is notified of new search results.
/// </summary>
public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler
{
	#region PRIVATE_MEMBER_VARIABLES

	// CloudRecoBehaviour reference to avoid lookups
	private CloudRecoBehaviour mCloudRecoBehaviour;
	// ImageTracker reference to avoid lookups
	private ObjectTracker mImageTracker;

	private bool mIsScanning = false;

	private string mTargetMetadata = "";

    String sorusayi = "";

    String sessayi = "";

    #endregion // PRIVATE_MEMBER_VARIABLES

    public AudioSource hayvanlar, at, kelebek, kus, maymun, sincap, kopek, meyveler, elma, armut, kiraz, cilek, domates, kivi, muz, fil;

	#region EXPOSED_PUBLIC_VARIABLES

	/// <summary>
	/// can be set in the Unity inspector to reference a ImageTargetBehaviour that is used for augmentations of new cloud reco results.
	/// </summary>
	public ImageTargetBehaviour ImageTargetTemplate;
  //  public ImageTargetBehaviour ImageTargetTemplate2;
    #endregion

    #region UNTIY_MONOBEHAVIOUR_METHODS

    /// <summary>
    /// register for events at the CloudRecoBehaviour
    /// </summary>
    void Start()
	{

        hayvanlar.Stop();
        at.Stop();
        kelebek.Stop();
        kus.Stop();
        maymun.Stop();
        sincap.Stop();
        kopek.Stop();
        meyveler.Stop();
        elma.Stop();
        armut.Stop();
        kiraz.Stop();
        cilek.Stop();
        domates.Stop();
        kivi.Stop();
        muz.Stop();
        fil.Stop();
        // register this event handler at the cloud reco behaviour
        CloudRecoBehaviour cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
		if (cloudRecoBehaviour)
		{
			cloudRecoBehaviour.RegisterEventHandler(this);
		}

		// remember cloudRecoBehaviour for later
		mCloudRecoBehaviour = cloudRecoBehaviour;

	}

	#endregion // UNTIY_MONOBEHAVIOUR_METHODS


	#region ICloudRecoEventHandler_IMPLEMENTATION

	/// <summary>
	/// called when TargetFinder has been initialized successfully
	/// </summary>
	public void OnInitialized()
	{
		// get a reference to the Image Tracker, remember it
		mImageTracker = (ObjectTracker)TrackerManager.Instance.GetTracker<ObjectTracker>();
	}

	/// <summary>
	/// visualize initialization errors
	/// </summary>
	public void OnInitError(TargetFinder.InitState initError)
	{
	}

	/// <summary>
	/// visualize update errors
	/// </summary>
	public void OnUpdateError(TargetFinder.UpdateState updateError)
	{
	}

	/// <summary>
	/// when we start scanning, unregister Trackable from the ImageTargetTemplate, then delete all trackables
	/// </summary>
	public void OnStateChanged(bool scanning) {
		mIsScanning = scanning;
		if (scanning) {
			// clear all known trackables
			ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			tracker.TargetFinder.ClearTrackables (false);
		}
	}

	/// <summary>
	/// Handles new search results
	/// </summary>
	/// <param name="targetSearchResult"></param>
	public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
	{
		// duplicate the referenced image target
		GameObject newImageTarget = Instantiate(ImageTargetTemplate.gameObject) as GameObject;
       // GameObject newImageTarget2 = Instantiate(ImageTargetTemplate2.gameObject) as GameObject;
        GameObject augmentation = null;
      //  GameObject augmentation2 = null;


        string model_name = targetSearchResult.MetaData;
/*
        if (augmentation2 != null)
            augmentation2.transform.parent = newImageTarget2.transform;*/

        if ( augmentation != null )
			augmentation.transform.parent = newImageTarget.transform;

		// enable the new result with the same ImageTargetBehaviour:
		ImageTargetAbstractBehaviour imageTargetBehaviour = mImageTracker.TargetFinder.EnableTracking(targetSearchResult, newImageTarget);
      //  ImageTargetAbstractBehaviour imageTargetBehaviour2 = mImageTracker.TargetFinder.EnableTracking(targetSearchResult, newImageTarget2);
        Debug.Log("Metadata value is " + model_name );
		mTargetMetadata = model_name;


		switch( model_name ){

		 

		case "Soru1" :
                {

				sorusayi="hayvanlar";
                  
                    break;
                }
            case "Soru2":
                {
				sorusayi="at";
                    break;
                }
            case "Soru3":
                {
				sorusayi="kelebek";
                    break;
                }
            case "Soru4":
                {
				sorusayi="kus";
                    break;
                }
            case "Soru5":
                {
				sorusayi="maymun";
                    break;
                }
            case "Soru6":
                {
				sorusayi="sincap";
                    break;
                }
            case "Soru7":
                {
				sorusayi="kopek";
                    break;
                }
		    case "Soru8":
			{
				sorusayi="fil";
				break;
			}
        
            case "cevap1":
                {
				sorusayi="elma";
                    break;
                }
            case "cevap2":
                {
				sorusayi="armut";
                    break;
                }
            case "cevap3":
                {
				sorusayi="kiraz";
                    break;
                }

            case "cevap4":
                {
				sorusayi="meyveler";
                    break;
                }
            case "cevap5":
                {
				sorusayi="cilek";
                    break;
                }
            case "cevap6":
                {
				sorusayi="domates";
                    break;
                }

            case "cevap7":
                {
				sorusayi="kivi";
                    break;
                }
			case "cevap8":
				{
				sorusayi="muz";
					break;
				}
          
        }

		string[] obje_dizi = new string[] {
			"hayvanlar",
			"at",
			"kelebek",
			"kus",
			"maymun",
			"sincap",
			"kopek",
			"fil",
			"kiraz",
			"elma",
			"armut",
			"muz",
			"kivi",
			"domates",
			"cilek",
			"meyveler"
		};

        int i = 0;
		foreach(string obje_isim in obje_dizi)
		{

         
            AudioSource[] sesdizi = new AudioSource[] {hayvanlar, at, kelebek, kus, maymun, sincap, kopek,fil, kiraz, elma, armut, muz ,kivi , domates, cilek, meyveler };

           


            if (!sorusayi.Equals(obje_isim))
            {
                Destroy(imageTargetBehaviour.gameObject.transform.Find(obje_isim).gameObject);

                sesdizi[i].Stop();
            }
            else
            {
                sesdizi[i].Play();
            }
            i++;
		}

        if (!mIsScanning)
		{
			// stop the target finder
			mCloudRecoBehaviour.CloudRecoEnabled = true;
		}
	}





	#endregion // ICloudRecoEventHandler_IMPLEMENTATION

	void OnGUI() {
		GUI.Box (new Rect(100,200,200,50), "Metadata: " + mTargetMetadata);
	}



}