using Counters;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject particles;

    [SerializeField] private GameObject stoveRedField;

    [SerializeField] private StoveCounter stoveCounter;


    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
    }
    private void StoveCounterOnOnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if (e.State == StoveCounter.State.Frying)
        {
            particles.SetActive(true);
            stoveRedField.SetActive(true);
        }
        else
        {
            particles.SetActive(false);
            stoveRedField.SetActive(false);
        }
    }
}