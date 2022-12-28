using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance ?? (instance = FindObjectOfType<GameManager>());

    [SerializeField] private Transform characterTransform, climbWallTransform;
    [SerializeField] private Slider slider;
    private float totalDistance;

    [SerializeField] private GameObject levelCompletedPanel;

    void Start()
    {
        totalDistance = climbWallTransform.position.z - characterTransform.position.z;
    }

    void Update()
    {
        CheckProgressBar();
    }

    public void OnGameCompleted()
    {
        levelCompletedPanel.SetActive(true);
    }

    private void CheckProgressBar()
    {
        var characterCurrentPosition = characterTransform.position.z;
        var currentDistance = totalDistance - characterCurrentPosition;
        var newSliderValue = (totalDistance - currentDistance) / totalDistance;
        slider.value = newSliderValue;
    }
}
