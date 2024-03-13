using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;


    private void Start()
    {
        CMPlayer.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
    }

    private void PlayerOnSelectedCounterChanged(object sender, CMPlayer.OnSelectedCounterChangedEventArgs e)
    {
        if (clearCounter == e.SelectedCounter)
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
        visualGameObject.SetActive(false);
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }
}