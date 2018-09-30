# CustomAuth

This is a sample to demostrate how to create a custom authentication scheme in asp.net core 2.1.

## CustomAuth.AuthService

This is a mock auth service, which provides one API to validate a token, and return user profile if token is valid.

```
GET /api/token/{token}
```

## CustomAuth.Client

This is a client of `CustomAuth.AuthService`, implemented as Typed HttpClient.

## CustomAuth.App1

This project demostrate how to create a custom AuthenticationHandler.

## CustomAuth.App2

This project plans to demostrate how to create a custom RemoteAuthenticationHandler.