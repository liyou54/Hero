using UnityEngine;

namespace GameMain.Framework.UI
{
    public class NodeEx : MonoBehaviour,IUIComponentEx
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