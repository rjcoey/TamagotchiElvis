using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(UIMover))]
public class TextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;

    [SerializeField] private float revealTime = 1.0f;


    private RectTransform rectTransform;
    private UIMover textBoxPanel;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textBoxPanel = GetComponent<UIMover>();
    }

    void Start()
    {
        textBox.text = "";
    }

    public IEnumerator TypeText(string text)
    {
        yield return Typewriter.TypewriterEffect(textBox, text);
    }

    public IEnumerator Reveal()
    {
        Vector2 endPosition = new(rectTransform.anchoredPosition.x, 0.0f);
        yield return textBoxPanel.LerpPanelPosition(rectTransform.anchoredPosition, endPosition, revealTime);
    }

    public IEnumerator Hide()
    {
        Vector2 endPosition = new(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.y);
        yield return textBoxPanel.LerpPanelPosition(rectTransform.anchoredPosition, endPosition, revealTime);
    }
}
