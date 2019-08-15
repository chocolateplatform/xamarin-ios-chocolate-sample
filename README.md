# Chocolate Sample - iOS

This sample demonstrates how to display different types of ads using Chocolate Mediation Ads SDK for Xamarin. The project builds as is, however, to display ads, you will first need to obtain an API Key from Chocolate. Contact us at sdk-support@vdopia.com.

Once you obtain the API Key, open the `AppDelegate.cs` file in the project and in the following line:

`ChocolatePlatform.InitWithAdUnitID("AAAAAA");`

Replace `AAAAAA` with the received API Key. The SDK is now ready to deliver ads.

The Chocolate SDK performs ad mediation among different partners, and delivers the best-yielding ad on each request. The partners for different ad types are as following:

1. Interstitial ads: Chocolate, AdColony, AppLovin, Amazon, Google Admob, and Unity
2. Rewarded ads: Chocolate, AdColony, AppLovin, Amazon, Google Admob, and Unity
3. Inview (300x250) ads: Chocolate, AppLovin, Amazon, and Google Admob

### Integrating the Chocolate SDK in your own Xamarin app

1. Add the Nuget package `xam.ios.chocolate_all` to the project. This package includes the compiled binaries for all the partners, as well as C# files in the `ChocolateSDK` namespace. You will need to include `using ChocolateSDK;` to any file where you use Chocolate's APIs

2. Initialize the SDK with your Chocolate API Key: `ChocolatePlatform.InitWithAdUnitID("AAAAAA");`. It is better to do this as early in the app launch cycle as possible, ideally in the `FinishedLaunching` method of `AppDelegate.cs`.

3. Implement the sets of callback methods to receive events from the SDK for the ad types you want to display. These are declared in the `IChocolatePlatformInterstitialAdDelegate`, `IChocolatePlatformRewardAdDelegate`, and `IChocolatePlatformInviewAdDelegate` interfaces.

4. Create the appropriate ad instances and call the APIs in the manner demonstrated within this project's `ViewController.cs` class.
