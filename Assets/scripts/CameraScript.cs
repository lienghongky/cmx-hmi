using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class CameraScript : MonoBehaviour
{
    
  
    private WebCamTexture webcamTexture;
    private RawImage display;

    private int SelectedCameraIndex = -1;
    private int DetectedCameras = 0;

#if UNITY_IOS || UNITY_WEBGL
    private bool CheckPermissionAndRaiseCallbackIfGranted(UserAuthorization authenticationType, Action authenticationGrantedAction)
    {
        if (Application.HasUserAuthorization(authenticationType))
        {
            if (authenticationGrantedAction != null)
                authenticationGrantedAction();

            return true;
        }
        return false;
    }

    private IEnumerator AskForPermissionIfRequired(UserAuthorization authenticationType, Action authenticationGrantedAction)
    {
        if (!CheckPermissionAndRaiseCallbackIfGranted(authenticationType, authenticationGrantedAction))
        {
            yield return Application.RequestUserAuthorization(authenticationType);
            if (!CheckPermissionAndRaiseCallbackIfGranted(authenticationType, authenticationGrantedAction))
                Debug.LogWarning($"Permission {authenticationType} Denied");
        }
    }
#elif UNITY_ANDROID
    private void PermissionCallbacksPermissionGranted(string permissionName)
    {
        StartCoroutine(DelayedCameraInitialization());
    }

    private IEnumerator DelayedCameraInitialization()
    {
        yield return null;
        InitializeCamera();
    }

    private void PermissionCallbacksPermissionDenied(string permissionName)
    {
        Debug.LogWarning($"Permission {permissionName} Denied");
    }

    private void AskCameraPermission()
    {
        var callbacks = new PermissionCallbacks();
        callbacks.PermissionDenied += PermissionCallbacksPermissionDenied;
        callbacks.PermissionGranted += PermissionCallbacksPermissionGranted;
        Permission.RequestUserPermission(Permission.Camera, callbacks);
    }
#endif

    void Start()
    {

    }
    void OnDisable()
    {
        webcamTexture.Stop();
    }

    void OnEnable()
    {
        // AskCameraPermissionAndInitialize();
    }



    public void AskCameraPermissionAndInitialize()
    {
        #if UNITY_IOS || UNITY_WEBGL
        StartCoroutine(AskForPermissionIfRequired(UserAuthorization.WebCam, () => { InitializeCamera(); }));
                return;
        #elif UNITY_ANDROID
                if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
                {
                    AskCameraPermission();
                    return;
                }
        #endif
                InitializeCamera();
    }

    private void InitializeCamera()
    {
        // Get list of available cameras
        WebCamDevice[] devices = WebCamTexture.devices;
        DetectedCameras = devices.Length;
        // Log available cameras
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log($"Camera {i}: {devices[i].name}");
        }

        if (devices.Length > 0)
        {
            // Use the first available camera (or choose based on user preference)
            string selectedCamera = devices[0].name;
            Debug.Log($"Selected camera: {selectedCamera}");
            // Initialize WebCamTexture with the selected camera
            webcamTexture = new WebCamTexture(selectedCamera);
           
        }
        else
        {
            Debug.LogError("No camera found");
            return;
        }
        
        display = GetComponent<RawImage>();
        Debug.Log("Display: " + display);
        display.texture = webcamTexture;
        webcamTexture.Play();
    }
    private void Update()
    {
        if (webcamTexture != null)
        {
            float ratio = (float)webcamTexture.width / (float)webcamTexture.height;
            // Debug.Log("display: " + display.texture.width + " " + display.texture.height);
            display.texture = webcamTexture;
        }
    }
    void OnDestroy()
    {
        // Stop the webcam when the object is destroyed
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }


    public void SwitchCamera ()
    {
        if (DetectedCameras < 2)
        {
            Debug.LogWarning("Only one camera detected. Cannot switch cameras.");
            return;
        }

        SelectedCameraIndex = (SelectedCameraIndex + 1) % DetectedCameras;
        string selectedCamera = WebCamTexture.devices[SelectedCameraIndex].name;
        Debug.Log($"Switching to camera {SelectedCameraIndex}: {selectedCamera}");
        webcamTexture.Stop();
        webcamTexture.deviceName = selectedCamera;
        webcamTexture.Play();
    }
}