using Microsoft.Bot.Builder;
using SampleEchoBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleEchoBot.Services
{
    //public class BotStateService
    //{
    //    //state variable
    //    public UserState UserState { get;  }

    //    //IDs
    //    public static string UserProfileId { get; } = $"{nameof(BotStateService)}.UserProfile";

    //    //Accessor
    //    public IStatePropertyAccessor<UserProfile> UserProfileAccessor { get; set; }


    //    public BotStateService(UserState userState)
    //    {
    //        UserState = userState ?? throw new ArgumentNullException(nameof(userState));
    //        InitializeAccessor();
    //    }

    //    private void InitializeAccessor()
    //    {
    //        //Initilize User State
    //        UserProfileAccessor = UserState.CreateProperty<UserProfile>(UserProfileId);
    //    }
    //}
}
