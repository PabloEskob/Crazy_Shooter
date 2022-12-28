using TMPro;
using UnityEngine;

namespace PlayfulSystems.ProgressBar
{
    public class BarViewValueText : ProgressBarProView
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        public TextMeshProUGUI TextMeshPro
        {
            get => _textMeshPro;
            set => _textMeshPro = value;
        }

        [SerializeField] string prefix = "";
        [SerializeField] float minValue = 0f;
        [SerializeField] private float _maxValue;

        [SerializeField] int numDecimals = 0;

        [SerializeField] bool showMaxValue = false;

        [SerializeField] string numberUnit = "%";

        [SerializeField] string suffix = "";

        public float MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        private float lastDisplayValue;

        public override bool CanUpdateView(float currentValue, float targetValue)
        {
            float displayValue = GetRoundedDisplayValue(currentValue);

            if (currentValue >= 0f && Mathf.Approximately(lastDisplayValue, displayValue))
                return false;

            lastDisplayValue = GetRoundedDisplayValue(currentValue);
            return true;
        }

        public override void UpdateView(float currentValue, float targetValue)
        {
            _textMeshPro.text = prefix + FormatNumber(GetRoundedDisplayValue(currentValue)) + numberUnit +
                                (showMaxValue ? " / " + FormatNumber(_maxValue) + numberUnit : "") + suffix;
        }

        float GetDisplayValue(float num)
        {
            return Mathf.Lerp(minValue, _maxValue, num);
        }

        float GetRoundedDisplayValue(float num)
        {
            float value = GetDisplayValue(num);

            if (numDecimals == 0)
                return Mathf.Round(value);

            float multiplier = Mathf.Pow(10, numDecimals);
            value = Mathf.Round(value * multiplier) / multiplier;

            return value;
        }

        string FormatNumber(float num)
        {
            return num.ToString("N" + numDecimals);
        }

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }
#endif
    }
}