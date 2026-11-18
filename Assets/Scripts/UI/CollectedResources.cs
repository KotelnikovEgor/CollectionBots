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
        _warehouse.ResourceAmountChanged += UpdateResourceText;
    }

    private void OnDisable()
    {
        _warehouse.ResourceAmountChanged -= UpdateResourceText;
    }

    private void UpdateResourceText(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Black:
                _BlackResourceText.text = amount.ToString();
                break;
            case ResourceType.Red:
                _RedResourceText.text = amount.ToString();
                break;
            case ResourceType.White:
                _WhiteResourceText.text = amount.ToString();
                break;
        }
    }
}
