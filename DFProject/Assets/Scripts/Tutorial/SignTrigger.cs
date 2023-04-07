using DG.Tweening;
using TMPro;
using UnityEngine;

public class SignTrigger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textOnSign;

    private Tween _tween;

    private void Start()
    {
        _textOnSign.DOFade(0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _tween.Kill();
            _tween = _textOnSign.DOFade(1f, 0.8f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _tween.Kill();
            _tween = _textOnSign.DOFade(0f, 0.8f);
        }
    }
}
