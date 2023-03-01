using System;
using UnityEngine;
using GameMain.Framework.UI;
using UnityGameFramework.Runtime;

namespace GameMain.Framework
{
     public class CardUITemplateData : UITemplateData
    {
        
	}
     public class CardUITemplate : UITemplateBase<CardUITemplateData>
    {
           [SerializeField] private  CardUIView view;
           public override void SetData(CardUITemplateData data)
           {
               base.SetData(data);
           }
    }
}