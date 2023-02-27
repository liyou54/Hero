using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain.Framework.UI
{
    public class ImageEx : Image, IUIComponentEx
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