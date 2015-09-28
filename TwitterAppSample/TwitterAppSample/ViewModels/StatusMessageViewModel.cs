using CoreTweet.Streaming;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAppSample.ViewModels
{
    public class StatusMessageViewModel
    {
        public StatusMessage Model { get; private set; }

        public ReactiveProperty<Uri> ProfileImage { get; private set; }

        public ReactiveProperty<string> ScreenName { get; private set; }

        public ReactiveProperty<string> Text { get; private set; }

        public StatusMessageViewModel()
        {
        }

        public StatusMessageViewModel(StatusMessage statusMessage)
        {
            this.Model = statusMessage;

            this.ProfileImage = ReactiveProperty.FromObject(
                statusMessage.Status.User,
                x => x.ProfileImageUrlHttps);
            this.ScreenName = ReactiveProperty.FromObject(
                statusMessage.Status.User,
                x => x.ScreenName);
            this.Text = ReactiveProperty.FromObject(
                statusMessage.Status,
                x => x.Text);
        }
    }
}
