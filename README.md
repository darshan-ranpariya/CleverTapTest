# Unity Weather App

This is a simple Unity application that demonstrates how to fetch and display weather information based on the user's location.

## Features

*   **Location-based Weather:** Retrieves the current temperature for the user's location.
*   **Cross-Platform Notifications:** Displays information using native UI components (Toast on Android, Snackbar on iOS).
*   **REST API Integration:** Fetches weather data from the Open-Meteo API.

## Android APK

A pre-built APK file is included in the root of this project. You can install this file directly onto an Android device to quickly test the application without needing to open the project in Unity and build it yourself.

## How to Use (From Source)

1.  Open the project in the Unity Editor.
2.  Open the scene located at `Assets/Scenes/SampleScene.unity`.
3.  Build and run on an Android or iOS device.
    *   **For Android:** Ensure you have an `AndroidManifest.xml` that requests `ACCESS_FINE_LOCATION` permission.
    *   **For iOS:** You will need to add a native implementation for the `_ShowSnackbar` function in the generated Xcode project.

## Project Structure

*   **Assets/Scripts/WeatherApp:** Contains the core logic for fetching and managing weather data.
    *   `WeatherManager.cs`: The main script that handles location services and API requests.
    *   `Models/`: Contains the data structures for parsing the API response.
*   **Assets/Scripts/CleverTapPackage:** A simulated package for handling native notifications.
    *   `NotificationService.cs`: Implements the platform-specific notification logic.
*   **Assets/Scripts/Test:** Contains unit tests for the application.
    *   `WeatherTests.cs`: Includes a test for JSON parsing to ensure the data model is correct.