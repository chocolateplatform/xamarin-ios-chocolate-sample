using Foundation;
using System;
using UIKit;
using ChocolateSDK;

namespace ChocolateSample
{
    public partial class ViewController : UIViewController, IChocolatePlatformInterstitialAdDelegate, IChocolatePlatformRewardAdDelegate
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        private ChocolatePlatformInterstitialAdDisplay iad;
        private ChocolatePlatformRewardAdDisplay rad;

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
    }
}