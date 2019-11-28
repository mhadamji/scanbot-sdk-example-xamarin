﻿using System;
using ReadyToUseUIDemo.iOS.Repository;
using ReadyToUseUIDemo.iOS.View;
using ScanbotSDK.iOS;
using UIKit;

namespace ReadyToUseUIDemo.iOS.Controller
{
    public class PreviewController : UIViewController
    {
        public PreviewPageView ContentView { get; private set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ContentView = new PreviewPageView();
            View = ContentView;

            ContentView.Collection.Pages.AddRange(PageRepository.Items);
            ContentView.Collection.ReloadData();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

    }
}
