<<<<<<< HEAD
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.3.0

using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;

namespace SampleEchoBot
{
    public class ConfigurationCredentialProvider : SimpleCredentialProvider
    {
        public ConfigurationCredentialProvider(IConfiguration configuration)
            : base(configuration["MicrosoftAppId"], configuration["MicrosoftAppPassword"])
        {
        }
    }
}
=======
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.3.0

using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;

namespace SampleEchoBot
{
    public class ConfigurationCredentialProvider : SimpleCredentialProvider
    {
        public ConfigurationCredentialProvider(IConfiguration configuration)
            : base(configuration["MicrosoftAppId"], configuration["MicrosoftAppPassword"])
        {
        }
    }
}
>>>>>>> 266dbfad7aa5f811859f3e35c2fe03618167859f
