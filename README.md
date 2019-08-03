
# Click-A-Tell-.NET
**This is an unoffical Click A Tell platform API for .NET**

## Install Package

## Usage

Add Namespaces

    using ClickATel;
    using ClickATel.Models;
    using ClickATel.Models.Results;
    
Configure API Key

    Authenticate.ApiKey = "8Jr1Jr9oQFGCV4wnCvHOnA==";
	Authenticate.Test_Login(true);//Test API Key
    
Setup [country code](https://countrycode.org/) and sending phone number

    Settings.CountryCode = 1; //Country code prefix
    Settings.DefaultFromNumber = "7195552020";
    
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
