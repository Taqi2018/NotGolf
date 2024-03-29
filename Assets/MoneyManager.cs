using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private float lastZPosition;

    public float moneyPerMeter = 1f; // Adjust this value based on how much money you want to give per meter traveled
    public float distanceScaleFactor = 50f; // Adjust this value based on the speed of your game

    void Start()
    {
        lastZPosition = transform.position.z;
    }

    void Update()
    {
        // Calculate the distance traveled along the z-axis since the last frame
        float distanceTraveledThisFrame = Mathf.Abs(transform.position.z - lastZPosition);

        // Scale the distance traveled to ensure even small distances result in money earned
        float scaledDistanceTraveled = distanceTraveledThisFrame * distanceScaleFactor;

        // Calculate the money earned based on the scaled distance traveled
        int moneyEarned = Mathf.RoundToInt(scaledDistanceTraveled * moneyPerMeter);

        // Add the earned money to the existing money value in PlayerPrefs
        int currentMoney = PlayerPrefs.GetInt("Money", 0);
        PlayerPrefs.SetInt("Money", currentMoney + moneyEarned);

        // Save PlayerPrefs
        PlayerPrefs.Save();

        // Update lastZPosition for the next frame
        lastZPosition = transform.position.z;
    }
}