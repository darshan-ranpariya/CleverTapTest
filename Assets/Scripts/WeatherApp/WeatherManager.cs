using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using CleverTapPackage;

public class WeatherManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private NotificationService _notificationService;

    // API URL with placeholders {0} and {1}
    private const string ApiUrl = "https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&timezone=auto&daily=temperature_2m_max";

    // LINK THIS TO YOUR UNITY BUTTON
    public void GetWeatherForCurrentLocation()
    {
        StartCoroutine(ProcessWeatherRequest());
    }

    private IEnumerator ProcessWeatherRequest()
    {
        float lat, lon;

#if UNITY_EDITOR
        _notificationService.ShowMessage("Fetching weather for test location (Editor)...");
        lat = 19.12f; // Hardcoded latitude for testing
        lon = 72.87f; // Hardcoded longitude for testing
        yield return null; // Give a frame for the message to show
#else
        _notificationService.ShowMessage("Locating...");

        // 1. Start Service (this will trigger the permission dialog)
        Input.location.Start();
        
        // 2. Wait for initialization
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // 3. Handle failure cases
        if (maxWait < 1)
        {
            _notificationService.ShowMessage("Location timed out.");
            yield break;
        }
        
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            _notificationService.ShowMessage("Location permission denied or service failed.");
            yield break;
        }

        // 4. Get Coords
        lat = Input.location.lastData.latitude;
        lon = Input.location.lastData.longitude;
        Input.location.Stop();
#endif

        // 5. Call API
        string uri = string.Format(ApiUrl, lat, lon);
        yield return FetchWeatherData(uri);
    }

    private IEnumerator FetchWeatherData(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    WeatherResponse data = JsonUtility.FromJson<WeatherResponse>(request.downloadHandler.text);

                    if (data.daily != null && data.daily.temperature_2m_max.Length > 0)
                    {
                        float temp = data.daily.temperature_2m_max[0];
                        string tz = data.timezone_abbreviation;
                        
                        // RESULT: "Temp: 28.4°C | GMT+5:30"
                        _notificationService.ShowMessage($"Temp: {temp}°C | {tz}");
                    }
                    else
                    {
                        _notificationService.ShowMessage("No weather data found.");
                    }
                }
                catch (Exception e)
                {
                    _notificationService.ShowMessage("JSON Parse Error");
                }
            }
            else
            {
                _notificationService.ShowMessage("Net Error: " + request.error);
            }
        }
    }
}