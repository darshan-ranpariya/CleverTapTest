using UnityEngine;
using System.Runtime.InteropServices;
using CleverTapPackage.Interfaces;

namespace CleverTapPackage
{
    public class NotificationService : MonoBehaviour, INativeNotification
    {
        // iOS Import
#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _ShowSnackbar(string message);
#endif

        // Requirement: Show Toast on Click
        private void OnMouseDown()
        {
            ShowMessage("Toast System Active! Click the UI Button to fetch weather.");
        }

        // The Main API
        public void ShowMessage(string message)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                ShowAndroidToast(message);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                ShowiOSSnackbar(message);
            }
            else
            {
                Debug.Log($"[TOAST]: {message}");
            }
        }

        private void ShowAndroidToast(string message)
        {
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                
                currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", currentActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
            catch (System.Exception e) { Debug.LogError(e.Message); }
        }

        private void ShowiOSSnackbar(string message)
        {
#if UNITY_IOS
            _ShowSnackbar(message);
#endif
        }
    }
}