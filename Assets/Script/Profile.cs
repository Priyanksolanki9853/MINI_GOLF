using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

public class PerformanceAnalyzer : MonoBehaviour
{
    // Variables to calculate average FPS
    private int frameCount = 0;
    private float deltaTime = 0.0f;
    private float fps;

    // Update interval for performance analysis (in seconds)
    public float updateInterval = 1.0f;

    private float timeElapsed = 0.0f;

    void Update()
    {
        // Frame rate calculations
        frameCount++;
        deltaTime += Time.unscaledDeltaTime;
        timeElapsed += Time.unscaledDeltaTime;

        if (timeElapsed >= updateInterval)
        {
            // Calculate FPS
            fps = frameCount / timeElapsed;

            // Log performance metrics
            LogPerformanceMetrics();

            // Reset counters
            frameCount = 0;
            timeElapsed = 0.0f;
        }
    }

    void LogPerformanceMetrics()
    {
        // Memory usage (in MB)
        float totalReservedMemory = Profiler.GetTotalReservedMemoryLong() / (1024 * 1024);
        float totalAllocatedMemory = Profiler.GetTotalAllocatedMemoryLong() / (1024 * 1024);
        float totalUnusedReservedMemory = Profiler.GetTotalUnusedReservedMemoryLong() / (1024 * 1024);

        // Rendering Metrics (approximation since draw calls are not directly exposed)
        int batchCount = UnityStats.batches;
        int drawCalls = batchCount; // Batches are closely related to draw calls in Unity.

        // Log data to the console
        Debug.Log($"FPS: {fps:F2}\n" +
                  $"Total Reserved Memory: {totalReservedMemory:F2} MB\n" +
                  $"Total Allocated Memory: {totalAllocatedMemory:F2} MB\n" +
                  $"Unused Reserved Memory: {totalUnusedReservedMemory:F2} MB\n" +
                  $"Draw Calls (Batches): {drawCalls}");
    }

    void OnGUI()
    {
        // Display FPS on the screen
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 300, 30), $"FPS: {fps:F2}", style);
    }
}
