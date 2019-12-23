using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;

namespace VHProgressBar
{
    [DesignTimeVisible(true)]
    public class HorizontalProgressBar : UIView
    {
        UIView progressView;
        UIViewPropertyAnimator animator;
        bool isAnimating;

        public HorizontalProgressBar() 
        {
            InitProgressView();
        }

        public HorizontalProgressBar(NSCoder coder) : base(coder)
        {
            InitProgressView();
        }

        UIColor bgColor = UIColor.White;
        [Export(nameof(BGColor)), Browsable(true)]
        public UIColor BGColor
        {
            get => bgColor;
            set
            {
                bgColor = value;
                ConfigureView();
            }
        }

        UIColor barColor = UIColor.FromRGBA(red: 52 / 255, green: 181 / 255, blue: 240 / 255, alpha: 1);
        [Export(nameof(BarColor)), Browsable(true)]
        public UIColor BarColor
        {
            get => barColor;
            set
            {
                barColor = value;
                ConfigureView();
            }
        }

        UIColor frameColor = UIColor.FromRGBA(red: 161 / 255, green: 161 / 255, blue: 161 / 255, alpha: 1);
        [Export(nameof(FrameColor)), Browsable(true)]
        public UIColor FrameColor
        {
            get => frameColor;
            set
            {
                frameColor = value;
                ConfigureView();
            }
        }

        nfloat frameBold = 0.1f;
        [Export(nameof(FrameBold)), Browsable(true)]
        public nfloat FrameBold
        {
            get => frameBold;
            set
            {
                frameBold = value;
                ConfigureView();
            }
        }

        nfloat pgHeight = 20f;
        [Export(nameof(PGHeight)), Browsable(true)]
        public nfloat PGHeight
        {
            get => pgHeight;
            set
            {
                pgHeight = value;
                ConfigureView();
            }
        }

        nfloat pgWidth = 200f;
        [Export(nameof(PGWidth)), Browsable(true)]
        public nfloat PGWidth
        {
            get => pgWidth;
            set
            {
                pgWidth = value;
                ConfigureView();
            }
        }

        private void InitProgressView()
        {
            progressView = new UIView();
            AddSubview(progressView);
        }

        private void ConfigureProgressView()
        {
            progressView.BackgroundColor = BarColor;
            progressView.Frame = new CGRect(progressView.Frame.Location.X, Bounds.Location.Y , 0, PGHeight);
            progressView.Layer.CornerRadius = PGHeight / 2;
        }

        private void ConfigureView()
        {
            BackgroundColor = BGColor;
            Layer.BorderWidth = FrameBold;
            Layer.BorderColor = FrameColor.CGColor;
            Frame = new CGRect(Frame.Location, new CGSize(PGWidth, PGHeight));
            Layer.CornerRadius = PGHeight / 2;
        }

        public void AnimateProgress(nfloat duration, nfloat progressValue)
        {
            if (0 > progressValue || progressValue > 1.0)
            {
                return;
            }
            ConfigureProgressView();
            UICubicTimingParameters timing = new UICubicTimingParameters(UIViewAnimationCurve.EaseIn);
            animator = new UIViewPropertyAnimator(duration, timing);
            animator.AddAnimations(() => progressView.Frame =
            new CGRect(progressView.Frame.Location, new CGSize(progressView.Frame.Size.Width +PGWidth*progressValue, progressView.Frame.Size.Height)));

            animator.StartAnimation();
        }

        public void StartAnimation(string type, nfloat duration)
        {
            if (isAnimating)
                return;
            switch (type)
            {
                case "normal":
                    RunAnimation(reverse: false, duration: duration);
                    break;
                case "reverse":
                    RunAnimation(reverse: true, duration: duration);
                    break;
                default:
                    break;
            }
        }

        void RunAnimation(bool reverse, nfloat duration)
        {
            ConfigureProgressView();
            animator = UIViewPropertyAnimator.CreateRunningPropertyAnimator(duration, delay: 0.0, UIViewAnimationOptions.CurveEaseInOut, () =>
            {
                SetAnimationRepeatCount(1000);
                SetAnimationRepeatAutoreverses(reverse);
                progressView.Frame = new CGRect(progressView.Frame.Location, new CGSize(progressView.Frame.Size.Width+PGWidth, progressView.Frame.Size.Height));
            }, null);
            isAnimating = true;
            animator.StartAnimation();
        }

        public void StopAnimation()
        {
            if (!isAnimating)
                return;

            isAnimating = false;
            animator.StopAnimation(true);
        }

        public nfloat GetProgress() => progressView.Frame.Size.Width;
    }
}
