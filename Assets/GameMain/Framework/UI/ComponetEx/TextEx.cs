using TMPro;
using UnityEngine;

namespace GameMain.Framework.UI
{
    public class TextEx:TextMeshProUGUI,IUIComponentEx
    {
        [SerializeField] GenCodeData GenCodeData;

        public bool IsGenCode
        {
            get => GenCodeData.IsGenCode;
        }

        public string CompName
        {
            get => GenCodeData.CompName;
        }
    }
}