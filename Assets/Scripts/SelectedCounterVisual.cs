using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;


    private void Start()
    {
        CMPlayer.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
    }

    private void PlayerOnSelectedCounterChanged(object sender, CMPlayer.OnSelectedCounterChangedEventArgs e)
    {
        if (baseCounter == e.SelectedCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        foreach (var o in visualGameObjectArray) o.SetActive(false);
    }

    private void Show()
    {
        foreach (var o in visualGameObjectArray) o.SetActive(true);
    }
}