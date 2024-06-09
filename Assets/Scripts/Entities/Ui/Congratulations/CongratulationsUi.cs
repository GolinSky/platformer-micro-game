using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.ScriptUtils.Dotween;

namespace Mario.Entities.Ui.Congratulations
{
    public class CongratulationsUi: Base.Ui
    {
        private const float FadeOutDuration = 0f;
        private const float FadeOutValue = 0f;
        private const float FadeInDuration = 3f;
        private const float FadeInValue = 1f;
        
        [SerializeField] private TextMeshProUGUI congratulationsText;
        [SerializeField] private Image congratulationsImage;

        protected Sequence sequence;
        
        protected override void OnShow()
        {
            base.OnShow();
            sequence.KillIfExist();
            sequence = DOTween.Sequence();

            sequence.Append(congratulationsText.DOFade(FadeOutValue, FadeOutDuration));
            sequence.Append(congratulationsImage.DOFade(FadeOutValue, FadeOutDuration));
            sequence.Append(congratulationsText.DOFade(FadeInValue, FadeInDuration));
            sequence.Join(congratulationsImage.DOFade(FadeInValue, FadeInDuration));
            sequence.AppendCallback(Close);
        }

        protected override void OnClose()
        {
            base.OnClose();
        }
    }
}