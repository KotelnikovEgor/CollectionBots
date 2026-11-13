using TMPro;
using UnityEngine;

public class CollectedResources : MonoBehaviour
{
    [SerializeField] private Warehouse _warehouse;

    [SerializeField] private TMP_Text _BlackResourceText;
    [SerializeField] private TMP_Text _RedResourceText;
    [SerializeField] private TMP_Text _WhiteResourceText;

    private void OnEnable()
    {
        _warehouse.ResourceCountChanged += UpdateResourceText;
    }

    private void OnDisable()
    {
        _warehouse.ResourceCountChanged -= UpdateResourceText;
    }

    private void UpdateResourceText(ResourceType type, int count)
    {
        switch (type)
        {
            case ResourceType.Black:
                _BlackResourceText.text = count.ToString();
                break;
            case ResourceType.Red:
                _RedResourceText.text = count.ToString();
                break;
            case ResourceType.White:
                _WhiteResourceText.text = count.ToString();
                break;
        }
    }
}
