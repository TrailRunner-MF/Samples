Here I describes the Visual Studio solution, MySocialLoginTrial.sln.

------------------------
1.The purpose of this solution
------------------------
This solution demonstrates calling a web service created with ASP.NET Core from an ASP.NET 4.0 Framework program to obtain account information of social networking sites such as Google, Facebook, and Microsoft while being authenticated on the device. 

There are two types of web services created in ASP.NET Core, one works as intended and the other does not work well.
Both programs are almost identical in their source code, but the result of service is different as I mentioned above. My investigation so far has not been able to figure out the cause of this. I've published this on Github for you to join me in my investigation.

------------------------
2.The solution consists of
------------------------
MySocialLoginTrial consists of the following three projects

------------------------
2-1.MySocialLoginService
------------------------
This is a web service to get information of social network accounts that are authenticated on the device. This feature is implemented in MyExternalLogins.cshtml and .cs files in the \Pages\SocialLogin folder of the project.

Internally we use the ASP.NET Core Identity feature. 'Identity' is incorporated by specifying authentication changes when creating this project.
(Reference: https://docs.microsoft.com/ja-jp/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio)

------------------------
2-2.OtherSocialLoginService
------------------------
The functionality and content of the program is almost identical to MySocialLoginService. However, this program does not specify the use of 'Identity' when creating the project, once it is created as a normal ASP.NET Core MVC application, the necessary Nuget Package is added and then the program content is added to match with MySocialLoginService. This program raises an error when getting the information of the social network account that is logged in at the device.

------------------------
2-3.SocialLoginWebAPITest
------------------------
This is an ASP.NET 4.0 WebForm application that I created to call the two web services above.
It uses a special Server Control for testing.
The test program is the following two methods found in the file, which can be found in the in the \App_Code\c1_SLWebAPITest.cs 
*tm11_CallExternalLogins
*tm12_CallOtherExternalLogins

------------------------
3.How to run the program
------------------------
1. The contents of socialloginsettings.json in the Root folder of MySocialLoginService and OtherSocialLoginService will be stored in your Google, Facebook, and Microsoft root folders. Overwrite it with your own application information for them.(reference:https://docs.microsoft.com/ja-jp/aspnet/core/security/authentication/social/?view=aspnetcore-3.1&tabs=visual-studio)

2. Activate MySocialLoginService and perform the migration and DB creation. Specifically, execute the following commands from the Nuget Package Manager Console.
add-migration initialize
update-database

3. As a result, the value of ConnectionStrings.DefaultConnection node in appsettings.json of MySocialLoginService will be rewritten.Overwrite this value to the same node in the appsettings.json of OtherSocialLoginService.

4. Now build the solution. Once the build goes through, specify "Multi-Startup Project" in the properties of the solution. Select the browser in which SocialLoginWebAPITest is running as three web applications will be launched when you run it.

5. First select "test01.Execute ExternalLogins-Order" as the "Target TestMethod", then select one of your Google, Facebook or Microsoft accounts, and click the "Execute Test" button Click.

6. A confirmation screen for the Post parameters will appear, check the contents and click Submit.

7. The program retrieves the specified SNS account information and displays it in the message area of the web service.

8. Next, select "test02.Execute Other ExternalLogins-Order" as the "Target TestMethod" and run the test in the same way. In this case, you will see an error message "Page not found" on the login page of the SNS.

------------------------
4.What we would like to ask of you
------------------------
I would like to know why the service in the OtherSocialLoginService project that I called in the second test is causing an error. The location causing the error is the OnPostLinkLoginAsync method in the MyExternalLogins.cshtml.cs file in the MyExternalLogins.cshtml.cs folder. After this generates and returns the Challenge result, it generates and returns the Challenge result and then generates an error at the transition point.