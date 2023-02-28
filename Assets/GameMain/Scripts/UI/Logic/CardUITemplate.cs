using System;
using UnityEngine;
using GameMain.Framework.UI;

 namespace GameMain.Framework
{
     public class CardUITemplateData : UITemplateData
    {
        
	}
     public class CardUITemplate : UITemplateBase<CardUITemplateData>
    {
           [SerializeField] private  CardUIView view;
    }
}