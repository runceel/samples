using Microsoft.Practices.Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using TwitterAppSample.Models;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml.Navigation;
using CoreTweet.Streaming;

namespace TwitterAppSample.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly TwitterClient model = TwitterClient.Instance;

        public ReactiveProperty<string> Tweet { get; private set; }

        public ReactiveProperty<string> TweetInputError { get; private set; }

        public ReactiveCommand TweetComand { get; private set; }

        public ReadOnlyReactiveCollection<StatusMessageViewModel> StatusMessages { get; private set; }

        public ReactiveProperty<StatusMessageViewModel> SelectedStatusMessage { get; private set; }

        public ReactiveCommand RetweetCommand { get; private set; }

        public MainPageViewModel()
        {
            this.Tweet = this.model
                .ToReactivePropertyAsSynchronized(x => x.Tweet,
                    ignoreValidationErrorValue:true)
                .SetValidateNotifyError((string x) =>
                {
                    if (string.IsNullOrWhiteSpace(x)) { return "入力してください"; }
                    if (x.Length > 140) { return "140文字を超えています"; }
                    return null;
                });
            this.TweetInputError = Observable.Merge(
                this.Tweet.ObserveErrorChanged.Where(x => x == null).Select(_ => default(string)),
                this.Tweet.ObserveErrorChanged.Where(x => x != null).Select(x => x.OfType<string>().FirstOrDefault()))
                .ToReactiveProperty();

            this.TweetComand = this.Tweet.ObserveHasErrors
                .Select(x => !x)
                .ToReactiveCommand();
            this.TweetComand.Subscribe(async _ =>
                await this.model.TweetAsync());

            this.StatusMessages = this.model.User
                .StatusMessages
                .ToReadOnlyReactiveCollection(x => new StatusMessageViewModel(x));

            this.SelectedStatusMessage = new ReactiveProperty<StatusMessageViewModel>();

            this.RetweetCommand = this.SelectedStatusMessage
                .Select(x => x != null)
                .ToReactiveCommand();
            this.RetweetCommand.Subscribe(async _ =>
                {
                    await this.model.User.RetweetAsync(this.SelectedStatusMessage.Value.Model);
                });
        }

        public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
            await this.model.AuthAsync(async (authorizeUri, callbackUri) =>
                {
                    var response = await WebAuthenticationBroker.AuthenticateAsync(
                        WebAuthenticationOptions.None,
                        authorizeUri,
                        callbackUri);
                    if (response.ResponseStatus != WebAuthenticationStatus.Success) { return null; }

                    try
                    {
                        // 手抜き実装
                        return response.ResponseData.Split('&')[1].Split('=')[1];
                    }
                    catch
                    {
                        return null;
                    }
                });
            this.model.User.Connect();
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
        }
    }
}
