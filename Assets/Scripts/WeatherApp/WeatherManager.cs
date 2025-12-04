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
    private const string ApiUrl = "https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&timezone=IST&daily=temperature_2m_max";

    // LINK THIS TO YOUR UNITY BUTTON
    public void GetWeatherForCurrentLocation()
    {
        StartCoroutine(ProcessWeatherRequest());
    }

    private IEnumerator ProcessWeatherRequest()
    {
        _notificationService.ShowMessage("Locating...");

        // 1. Check Permission
        if (!Input.location.isEnabledByUser)
        {
            _notificationService.ShowMessage("Location permission denied.");
            yield break;
        }

        // 2. Start Service
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            _notificationService.ShowMessage("Location failed.");
            yield break;
        }

        // 3. Get Coords
        float lat = Input.location.lastData.latitude;
        float lon = Input.location.lastData.longitude;
        Input.location.Stop();

        // 4. Call API
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