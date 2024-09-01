﻿using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampRcon.Controls.HyperlinkLabel
{
    public class HyperlinkLabel : Label
    {
        public static readonly BindableProperty UrlProperty = BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkLabel), null);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public HyperlinkLabel()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = Color.Blue;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    try
                    {
                        await Launcher.OpenAsync(Url);
                    }
                    catch (Exception ex)
                    {
                        var properties = new Dictionary<string, string> {
                            { "Url", Url },
                            { "ExMessage", ex.Message }
                         };
                    }
                })
            });

        }
    }
}