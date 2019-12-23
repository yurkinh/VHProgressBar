using CoreGraphics;
using Foundation;
using System;
using UIKit;
using VHProgressBar;

namespace ProgressBarTest
{
    public partial class ViewController : UIViewController
    {
        
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var verticalProgressBar = new VerticalProgressBar();
            var horizontalProgressBar = new HorizontalProgressBar();
            View.AddSubview(verticalProgressBar);
            View.AddSubview(horizontalProgressBar);

            //adjust layout
            verticalProgressBar.Frame = new CGRect(new CGPoint(100, 100), verticalProgressBar.Frame.Size);
            verticalProgressBar.FrameBold = 0.5f;
            verticalProgressBar.BGColor = UIColor.Green;
            verticalProgressBar.BarColor = UIColor.Red;
            verticalProgressBar.FrameColor = UIColor.Black;

            horizontalProgressBar.Frame = new CGRect(new CGPoint(100, 500), horizontalProgressBar.Frame.Size);
            horizontalProgressBar.FrameBold = 0.1f;
            horizontalProgressBar.BGColor = UIColor.Yellow;
            horizontalProgressBar.BarColor = UIColor.Blue ;
            horizontalProgressBar.FrameColor = UIColor.Black;

            // start simple animation
            verticalProgressBar.AnimateProgress(duration: 2.0f, progressValue: 0.7f);
            horizontalProgressBar.AnimateProgress(duration: 2.0f, progressValue: 0.5f);

            // start repeat animation
            // you can choose type "normal" or "reverse"
            //horizontalProgressBar.StartAnimation(type: "normal", duration: 2.0f);
            //stop repeat animation  
            //horizontalProgressBar.StopAnimation();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}