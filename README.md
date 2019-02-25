# XeroProjectsApi
Xero Projects API

At the time of writing this Xero currently has no supported SDK for its Xero Projects.

Most of the code comes from the Supported Xero SDK https://github.com/XeroAPI/Xero-Net

Xero Projects API documentation can be found here: https://developer.xero.com/documentation/projects/overview-projects

*Currently only supporting Private Xero Apps*

Usage: 
```
\\Create Project
var projectHttpClient = new ProjectsHttpClient("<PUBLIC KEY>", @"<CERT PATH>", "<CERT PASSWORD>");
var Json = "{ name: "Example Project", contactId: "0000457b-ce16-44cc-9ee3-fea5a2c0000" };
projectHttpClient.HttpPost(jsonString);
```

In the future I will create contract C# clases which will be serialize into JSON.
