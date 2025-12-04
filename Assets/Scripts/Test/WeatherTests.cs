using NUnit.Framework;
using UnityEngine;

public class WeatherTests
{
    private const string SampleJson = "{\"latitude\":19.125,\"longitude\":72.875,\"generationtime_ms\":0.035643577575683594,\"utc_offset_seconds\":19800,\"timezone\":\"IST\",\"timezone_abbreviation\":\"GMT+5:30\",\"elevation\":6.0,\"daily_units\":{\"time\":\"iso8601\",\"temperature_2m_max\":\"°C\"},\"daily\":{\"time\":[\"2025-12-05\",\"2025-12-06\"],\"temperature_2m_max\":[28.4,29.0]}}";

    [Test]
    public void TestJsonParsing_MatchesSpecificSchema()
    {
        WeatherResponse response = JsonUtility.FromJson<WeatherResponse>(SampleJson);

        Assert.IsNotNull(response);
        Assert.AreEqual(19.125f, response.latitude);
        Assert.AreEqual("IST", response.timezone);
        Assert.AreEqual("GMT+5:30", response.timezone_abbreviation);
        
        Assert.IsNotNull(response.daily);
        Assert.AreEqual(28.4f, response.daily.temperature_2m_max[0]);
    }
}