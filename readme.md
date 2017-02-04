# TVShowTime API

[![Join the chat at https://gitter.im/tvshowtime-api-pcl/Lobby](https://badges.gitter.im/tvshowtime-api-pcl/Lobby.svg)](https://gitter.im/tvshowtime-api-pcl/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build status](https://ci.appveyor.com/api/projects/status/qgr5tvr90j8i1pu2?svg=true)](https://ci.appveyor.com/project/Odonno/tvshowtime-api-pcl)

A .NET library to access the TVShowTime API.

Supported platforms :

- .NET Framework 4.5
- Windows 8, Windows 8.1
- Windows Phone 8.1
- Windows 10 (UWP)
- Xamarin.iOS
- Xamarin.Android (not available - 1 issue to fix)

## Documentation

Take a look at the TVShowTime API documentation here : https://api.tvshowtime.com/doc

### How to use ? (normal implementation)

Initialize an instance of `TVShowTimeApiService` and you are ready to use the TVShowTime API.
First, retrieve token through Authentication/Login process and then call API endpoints with methods available in the API service.

#### Authentication

To retrieve a token and access API endpoints, you have to provide the client id, client secret of your application (OAuth credentials).

```
var apiService = new TVShowTimeApiService();
bool? isAuthenticated = await apiService.LoginAsync("<your-client-id>", "<your-client-secret>");
```

If `isAuthenticated == true`, it means you have been successfully authenticated and the token provided allows you to make API calls.
In other case, something went wrong during the authentication process.

#### Handling exceptions

Do not forget that your API can be break. So, here is a way to handle exceptions using the normal implementation.

```
var apiService = new TVShowTimeApiService();
try
{
    bool? isAuthenticated = await apiService.LoginAsync("<your-client-id>", "<your-client-secret>");
}
catch (Exception ex)
{
    // TODO
}
```

### How to use ? (reactive implementation)

Initialize an instance of `ReactiveTVShowTimeApiService` and you are ready to use the TVShowTime API using the Reactive Extensions paradigm.
First, retrieve token through Authentication/Login process and then call API endpoints with methods available in the API service.

#### Authentication

To retrieve a token and access API endpoints, you have to provide the client id, client secret of your application (OAuth credentials).

```
var reactiveApiService = new ReactiveTVShowTimeApiService();
reactiveApiService.Login("<your-client-id>", "<your-client-secret>")
    .Subscribe((isAuthenticated) => 
    {
        // TODO
    },
    (error) =>
    {
        // TODO
    });
```

If `isAuthenticated == true`, it means you have been successfully authenticated and the token provided allows you to make API calls.
In other case, something went wrong during the authentication process.

#### Handling exceptions

Do not forget that your API can be break. So, here is a way to handle exceptions using the reactive implementation.
Using Reactive Extensions, you can retrieve exceptions thrown in the `OnError` handler.
So, `error` variable is the Exception that has been thrown inside the service.

```
var reactiveApiService = new ReactiveTVShowTimeApiService();
reactiveApiService.Login("<your-client-id>", "<your-client-secret>")
    .Subscribe((isAuthenticated) => 
    {
        // TODO
    },
    (error) =>
    {
        // TODO : `error` is the Exception that has been thrown
    });
```