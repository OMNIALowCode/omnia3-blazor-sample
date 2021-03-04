# OMNIA "Order Management" Demo App

This repository features a Singe Page Application (SPA) that uses [OMNIA](https://omnialowcode.com) as its [Open Id Connect](https://openid.net/connect/) authentication provider. This web app also uses OMNIA as its backend, consuming the OMNIA (REST) API.


The [Blazor Webassembly Standalone](https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/standalone-with-authentication-library?view=aspnetcore-5.0&tabs=visual-studio) framework was chosen for this example.

NB: This example assumes the readers familiarity with OMNIA development.

# 1. Getting started:
## 1.1. OMNIA Configuration

### 1.1.1. API Client Creation

It is necessary to configure your OMNIA Platform with a new "Web App" API Client. This configuration will enable users to authenticate themselves on the "Order Management" app through the OMNIA sign-in page:

1. Access the "API Clients" page located on the OMNIA management area.

2. Create a new "WebApp" client. By default, this "Order Management" Webapp will be running on the port 5551 of your machine, so the values for the client configuration are:

|Property|Value|
|---|---|
|Redirect URI|https://localhost:5551/authentication/login-callback|
|Allowed CORS Origins|https://localhost:5551|
|Post-Logout Redirect URI|https://localhost:5551|

3. Click "save" and copy the client id that is generated. You will need this value afterwards.

### 1.1.2. Tenant setup

1. Create a new Tenant (e.g. code: "BlazorDemo")

2. Access the tenant's modeling area:

    1. Import the model .zip file

    2. Build and Deploy this tenant

3. Access the tenant's application area:

    1. Create a new "Order Serie"

    
## 1.2. Start up the "Order Management" App

In order to run this Blazor Webassembly app, you need to have dotnet 5 installed on your machine. We also recommend that you use Visual Studio Code for the following steps:

1. Edit the wwwroot/appsettings.Development.json changing the following values:
    - Authority : This URI must point your platform's identity address. If OMNIA Platform is running on your machine (e.g. using docker development environment), this URI will be http://host.docker.internal/identity .
    - ClientId : The value that was generated after the configuration of the new API client on OMNIA.
    - ApiBaseURI : The base URI where your OMNIA API is running. When running OMNIA Platform locally using the docker development environment, this will be http://host.docker.internal:5000/api/v1/
    - TenantCode : The OMNIA Tenant code 


2. To start the app, open a command line on the root of this project and run the following commands:
    1. `dotnet restore` - To install all the project's dependencies;
    2. `dotnet build` -  To compile the source code;
    3. `dotnet run` - To run the compiled binaries, starting the local http server. Alternatively, you can run `dotnet watch run` to automatically apply the changes you make to the source code of the blazor app without having to re-compile and rerunning the dotnet project.

3. Open your browser on https://localhost:5551 . If you are prompted to do so, accept the HTTPS certificate. Once the page is loaded, click "Log in" on the top-right corner. You will be redirected to the OMNIA sign-in page and, after you successfully login, be redirected to the "Order Management" web app.

4. All set! The "Order Management" app should now be up and running on your machine. We suggest that you create a couple of products and salespersons before trying to create an order.

# 2. Make some changes to the app

Now that you can use the sample "Order Management" app, we encourage you to make some changes to the code. The project structure is organized on the following way:

- Pages: The location of the blazor components that compose make the app pages. 
- Shared : This is the folder where the source C# code must live. It also contains the blazor components that are shared between pages. This folder is divided into:
    - DTO: Were the classes that correspond to the OMNIA model entities are located. 
    - Model: A set of utility classes that facilitate the integration of this app with the OMNIA API. 

We recommend that you start by analyzing how the "Product List" page component (Pages/ProductListPage.razor) is built before proceeding into more complex scenarios e.g. Order/Product creation.

# 3. Deploy your app

If you want to deploy the web app it is as simple as:

1. Run the following commands on the project root directory:
    - `dotnet publish .\omnia-blazor-demo.csproj -c Release`
2. Copy the "bin/Release/net5.0/publish/wwwroot" directory into a static web-host provider. (E.g. [Netlifly](https://www.netlify.com/) free service)

Make sure to update the wwwroot/appSettings.json and OMNIA's web app api client configuration accordingly to the deployment url. Otherwise the authentication process will not work.


