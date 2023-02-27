using UnityGameFramework.Runtime;

namespace GameMain.Scripts.Tools
{
    public static class UIComponentEx
    {
        public static int OpenUIForm(this UIComponent uiComponent, UINameAlais uiNameAlais, object userData = null)
        {
            var uiFormAssetName = GameEntry.GetComponent<DataTableComponent>().GetDataTable<UITable>().GetDataRow((int) uiNameAlais);
            return uiComponent.OpenUIForm(uiFormAssetName.Path,uiFormAssetName.Group.ToString(), userData);
        }
    }
}