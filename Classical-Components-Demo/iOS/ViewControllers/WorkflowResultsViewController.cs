// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using ScanbotSDK.iOS;
using System.Collections.Generic;
using CoreGraphics;

namespace ClassicalComponentsDemo.iOS
{
    public partial class WorkflowResultsViewController : UIViewController
    {
        public WorkflowResultsViewController(IntPtr handle) : base(handle)
        {
        }

        SBSDKUIWorkflowStepResult[] WorkflowResults;

        public static WorkflowResultsViewController InstantiateWith(SBSDKUIWorkflowStepResult[] workflowResults)
        {
            UIStoryboard storyboard = UIStoryboard.FromName("Main", null);
            WorkflowResultsViewController controller = (WorkflowResultsViewController)storyboard.InstantiateViewController("WorkflowResultsViewController");
            controller.WorkflowResults = workflowResults;
            return controller;
        }

        override public void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.collectionView.WeakDataSource = this;
            this.collectionView.WeakDelegate = this;
            this.UpdateResults();
        }

        void UpdateResults()
        {
            this.collectionView.ReloadData();

            String resultsText = this.ResultText();
            this.textView.Text = resultsText;
            this.toPasteboardButton.Enabled = resultsText.Length > 0;
        }

        String ResultText()
        {
            List<String> texts = new List<string>(this.WorkflowResults.Length);

            foreach (var result in this.WorkflowResults)
            {
                if (result.MrzResult != null)
                {
                    texts.Add(result.MrzResult.StringRepresentation);
                }

                if (result.DisabilityCertificateResult != null)
                {
                    texts.Add(result.DisabilityCertificateResult.StringRepresentation);
                }

                if (result.BarcodeResults != null && result.BarcodeResults.Length > 0)
                {
                    foreach (var code in result.BarcodeResults)
                    {
                        texts.Add(code.StringValue);
                        texts.Add("\n\n");
                    }
                }

                if (result.PayformResult != null && result.PayformResult.RecognizedFields.Length > 0)
                {
                    foreach (var field in result.PayformResult.RecognizedFields)
                    {
                        if (field.ToString().Length > 0)
                        {
                            texts.Add(field.ToString());
                        }
                    }
                }

            }

            String text = "";

            foreach (var t in texts)
            {
                text += t + "\n\n";
            }

            return text;
        }

        partial void toPasteboardButtonTapped(UIButton sender)
        {
            UIPasteboard.General.String = this.ResultText();
        }

        partial void closeButtonTapped(UIButton sender)
        {
            this.PresentingViewController.DismissViewController(true, null);
        }

        [Export("numberOfSectionsInCollectionView:")]
        public nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        [Export("collectionView:numberOfItemsInSection:")]
        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return this.WorkflowResults.Length;
        }

        [Export("collectionView:cellForItemAtIndexPath:")]
        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            WorkflowResultsCollectionViewCell cell = (WorkflowResultsCollectionViewCell)collectionView.DequeueReusableCell("resultsCell", indexPath);
            SBSDKUIWorkflowStepResult result = this.WorkflowResults[indexPath.Row];
            UIImage image = result.Thumbnail;
            cell.ImageView.Image = image;
            return cell;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            nfloat totalWidth = collectionView.Frame.Size.Width - 32.0f;
            nfloat cellWidth = 128.0f;
            nint numItems = this.WorkflowResults.Length;

            if (numItems == 1)
            {
                cellWidth = totalWidth;
            }
            else
            {
                nfloat spacing = (numItems - 1) * 10.0f;
                cellWidth = (nfloat)Math.Max(128.0f, (totalWidth - spacing) / numItems);
            }

            return new CGSize(cellWidth, 128);
        }
    }

}