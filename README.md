# Unity Weather App

This is a simple Unity application that demonstrates how to fetch and display weather information based on the user's location.

## Features

*   **Location-based Weather:** Retrieves the current temperature for the user's location.
*   **Cross-Platform Notifications:** Displays information
 using native UI components (Toast on Android, Snackbar on iOS).
*   **REST API Integration:** Fetches weather data from the Open-Meteo API.

## How to Use

1.  Open the project in the Unity Editor.
2.  Create a new scene.
3.  Create an empty GameObject and attach the `WeatherManager.cs` script to it.
4.  Create another empty GameObject and attach the `NotificationService.cs` script to it.
5.  In the `WeatherManager` component, drag the GameObject with the `NotificationService` script into the `_
notificationService` field.
6.  Create a UI Button in the scene.
7.  In the Button's `OnClick()` event, add a new event.
8.  Drag the GameObject with the `WeatherManager` script into the object field.
9.  From the function dropdown,
 select `WeatherManager` -> `GetWeatherForCurrentLocation()`.
10. Build and run on an Android or iOS device.

## Project Structure

*   **Assets/Scripts/WeatherApp:** Contains the core logic for fetching and managing weather data.
    *   `WeatherManager.cs`: The main script
 that handles location services and API requests.
    *   `Models/`: Contains the data structures for parsing the API response.
*   **Assets/Scripts/CleverTapPackage:** A simulated package for handling native notifications.
    *   `NotificationService.cs`: Implements the platform-specific notification logic.