using CoreTweet;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAppSample.Models
{
    class TwitterClient : BindableBase
    {
        public static readonly TwitterClient Instance = new TwitterClient();

        private readonly User user = new User();

        public User User
        {
            get { return this.user; }
        }

        public void Initialize()
        {
            this.User.LoadToken();
        }

        public async Task AuthAsync(Func<Uri, Uri, Task<string>> getPin)
        {
            if (this.User.IsAuth) { return; }

            var session = await OAuth.AuthorizeAsync(Constants.ConsumerKey, Constants.ConsumerSecret, Constants.CallbackUrl);
            var pin = await getPin(session.AuthorizeUri, new Uri(Constants.CallbackUrl));

            if (string.IsNullOrWhiteSpace(pin)) { return; }

            this.User.SaveToken(await session.GetTokensAsync(pin));
        }

        private string tweet;

        public string Tweet
        {
            get { return this.tweet; }
            set { this.SetProperty(ref this.tweet, value); }
        }


        public async Task TweetAsync()
        {
            var x = this.Tweet;
            this.Tweet = "";

            await this.User.TweetAsync(x);
        }
    }
}
