# Click-A-Tell-.NET
**This is an unoffical Click A Tell platform API for .NET**

## Install Package
https://www.nuget.org/packages/ClickATellAPI/

In Visual Studio goto View -> Other Windows -> Package manager and type
  
    Install-Package ClickATellAPI -Version 1.0.0

**or**

1. Goto Project -> Manage NuGet packages
2. Click Browse
3. Search ClickATellAPI
4. On the right hand side click install 


## Usage

Add Namespaces

    using ClickATel;
    using ClickATel.Models;
    using ClickATel.Models.Results;
    
Configure API Key

    Authenticate.ApiKey = "8Jr1Jr9oQFGCV4wDfaEnA==";
	Authenticate.Test_Login(true);//Test API Key
    
Setup [country code](https://countrycode.org/) and sending phone number

    Settings.CountryCode = 1; //Country code prefix
    Settings.DefaultFromNumber = "7195552020";//ONLY FOR TWO WAY INTEGRATIONS
    
Get Account Balance

    decimal balance = Account.GetBalance(out string currency);
    
Send message to a single recipient 

    SendMessageStatus SendStatus =  MessagesActions.SendMessage("Hello World!","7195553652");
    
Send message to multiple recipients

    SendMessagesStatus SendStatus =  MessagesActions.SendMessage("Hello World!", "7195552026" , "7195553652");
    
Send message at a specific time

    SendMessageStatus SendStatus1 = MessagesActions.SendMessage("Hello World!", DateTime.Now.AddHours(1) ,"7195553652");
    
Get message status from gateway

    string Msg_Status = GetMessageStatus(SendStatus1);

## Debugging

[Click-A-Tel Error Discriptions](https://www.clickatell.com/developers/api-documentation/rest-api-error-message-descriptions/)

You can find the error message under the `SendMessageStatus` class's error property

**Example**

    Console.WriteLine(SendStatus.error);

## How to get your API keys

1) Login to your click a tell account 
2) Copy your sms API key 

![Api Key](https://imgur.com/OgE6oQ6.png)
3. Scroll down and copy your from phone number

![From Number](https://imgur.com/KIwExPO.png)
