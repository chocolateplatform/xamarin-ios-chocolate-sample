using Foundation;
using System;
using UIKit;
using ChocolateSDK;

namespace ChocolateSample
{
    public partial class ViewController : UIViewController,
        IChocolatePlatformInterstitialAdDelegate,
        IChocolatePlatformRewardAdDelegate,
        IChocolatePlatformInviewAdDelegate
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        private ChocolatePlatformInterstitialAdDisplay iad;
        private ChocolatePlatformRewardAdDisplay rad;
        private ChocolatePlatformInviewAdDisplay ivad;

        private double bannerAdVerticalPos;

        public UIViewController RewardAdViewControllerForPresentingModalView => this;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            LoadIntBtn.TouchUpInside += (object sender, EventArgs e) => { LoadIntAd(); };
            ShowIntBtn.TouchUpInside += (object sender, EventArgs e) => { ShowIntAd(); };
            ShowIntBtn.Enabled = false;
            LoadRwdBtn.TouchUpInside += (object sender, EventArgs e) => { LoadRwdAd(); };
            ShowRwdBtn.TouchUpInside += (object sender, EventArgs e) => { ShowRwdAd(); };
            ShowRwdBtn.Enabled = false;
            InviewAdBtn.TouchUpInside += (object sender, EventArgs e) => { InviewAdAction(); };

            //Inview ad will appear 10 points below Inview Ad Button: 10 point 
            //gap + 125, which is half of the ad's own height of 250
            bannerAdVerticalPos =
                InviewAdBtn.Frame.X +
                InviewAdBtn.Frame.Height +
                135.0;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        //Mark - UI
        public void LoadIntAd()
        {
            iad = new ChocolatePlatformInterstitialAdDisplay(ChocolatePlatform.AdUnitID, this);
            iad.Load();
        }

        public void ShowIntAd()
        {
            iad.ShowFrom(this);
        }

        public void LoadRwdAd()
        {
            rad = new ChocolatePlatformRewardAdDisplay(ChocolatePlatform.AdUnitID, this);
            rad.Load();
        }

        public void ShowRwdAd()
        {
            ChocolatePlatformRewardAdSettings settings = ChocolatePlatformRewardAdSettings.BlankSettings();
            settings.RewardAmount = 100;
            settings.RewardName = "hippos";
            rad.Show(settings);
        }

        // interstitial delegate interface

        public void OnInterstitialLoaded(ChocolatePlatformInterstitialAdDisplay interstitialAd)
        {
            ShowIntBtn.Enabled = true;
        }

        public void OnInterstitialFailed(ChocolatePlatformInterstitialAdDisplay interstitialAd, ChocolatePlatformNoAdReason errorCode)
        {
            Console.Write("failed to load interstitial");
        }

        public void OnInterstitialShown(ChocolatePlatformInterstitialAdDisplay interstitialAd)
        {
            Console.Write("interstitial shown");
        }

        public void OnInterstitialClicked(ChocolatePlatformInterstitialAdDisplay interstitialAd)
        {
            Console.Write("interstitial clicked");
        }

        public void OnInterstitialDismissed(ChocolatePlatformInterstitialAdDisplay interstitialAd)
        {
            ShowIntBtn.Enabled = false;
        }

        //reward ad delegate
        public void RewardedVideoDidLoadAd(ChocolatePlatformRewardAdDisplay rewardedAd)
        {
            ShowRwdBtn.Enabled = true;
        }

        public void RewardedVideoDidFailToLoadAdWithError(int error, ChocolatePlatformRewardAdDisplay rewardedAd)
        {
            Console.Write("failed to load reward");
        }

        public void RewardedVideoDidStartVideo(ChocolatePlatformRewardAdDisplay rewardedAd)
        {
            Console.Write("starting rewarded video");
        }

        public void RewardedVideoDidFailToStartVideoWithError(int error, ChocolatePlatformRewardAdDisplay rewardedAd)
        {
            Console.Write("failed to start reward");
        }

        public void RewardedVideoWillDismiss(ChocolatePlatformRewardAdDisplay rewardedAd)
        {

        }

        public void RewardedVideoDidFinish(nuint rewardAmount, string rewardName)
        {
            ShowRwdBtn.Enabled = false;
        }

        //Mark: - Inview Ad

        public void InviewAdAction()
        {
            if(ivad == null) { //need to show ad
                ivad = new ChocolatePlatformInviewAdDisplay(
                    ChocolatePlatform.AdUnitID,
                    ChocolatePlatformAdSize.Banner,
                    this);
                ivad.LoadAd();
            } else { //need to close ad
                ivad.RemoveFromSuperview();
                ivad = null;
            }
        }

        public void OnBannerAdLoaded(ChocolatePlatformInviewAdDisplay banner)
        {
            ivad.ShowIn(this.View, new CoreGraphics.CGPoint(this.View.Center.X, bannerAdVerticalPos));
            InviewAdBtn.SetTitle("Close Inview Ad", UIControlState.Normal);
        }

        public void OnBannerAdFailed(ChocolatePlatformInviewAdDisplay banner, int errorCode)
        {
            Console.Write("Inview Ad Load Failed");
            ivad = null;
        }

        public void OnBannerAdClicked(ChocolatePlatformInviewAdDisplay banner)
        {
            Console.Write("Inview Ad Clicked");
        }

        public void OnBannerAdDissmiss(ChocolatePlatformInviewAdDisplay banner)
        {
            InviewAdBtn.SetTitle("Show Inview Ad", UIControlState.Normal);
        }
    }
}