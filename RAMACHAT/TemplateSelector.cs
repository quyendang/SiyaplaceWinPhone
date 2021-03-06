﻿using RAMACHAT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RAMACHAT
{
    public abstract class TemplateSelector : ContentControl
    {
        public abstract DataTemplate SelectTemplate(object item, DependencyObject container);

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }
    }
    public class ContactTemplateSelector : TemplateSelector
    {
        public DataTemplate ImageLeft
        {
            get;
            set;
        }

        public DataTemplate ImageRight
        {
            get;
            set;
        }
        public DataTemplate PicLeft
        {
            get;
            set;
        }
        public DataTemplate PicRight
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var i = item as ItemViewModel;
            if (i.SenderID == App.client.USERID)
            {
                if(i.Type ==1)
                {
                    return ImageRight;
                }
                else
                {
                    return PicLeft;
                }
                
            }
            else
            {
                if(i.Type == 1)
                {
                    return ImageLeft;
                }
                else
                {

                    return PicRight;
                }
            }
        }
    }
}
