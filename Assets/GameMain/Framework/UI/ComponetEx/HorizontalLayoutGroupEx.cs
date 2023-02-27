using UnityEngine;
using UnityEngine.UI;

namespace GameMain.Framework.UI
{
    public class HorizontalLayoutGroupEx:HorizontalLayoutGroup,IUIComponentEx
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