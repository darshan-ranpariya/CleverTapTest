using System;

[Serializable]
public class WeatherResponse
{
    public float latitude;
    public float longitude;
    public string timezone;              // "IST"
    public string timezone_abbreviation; // "GMT+5:30"
    public DailyData daily;
}
