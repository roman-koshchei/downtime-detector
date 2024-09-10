# Downtime Detector

Tool to check if there is downtime with various periods of checking.
You set period of requests and url to send `GET` requests.
If response has `2xx` status code then website is considered to be up.

Currently it's a CLI that just outputs greed or red `|` to indicate weather resource is up or not.

I use it during development of zero-downtime deployments, so I can confirm it's truly zero-downtime.

## Run

```bash
dotnet run --project ./src/Cli/Cli.csproj
```

## Build release

### Windows

Build:

```bash
dotnet publish -r win-x64 -c Release
```

Run:

```bash
./src/Cli/bin/Release/net8.0/win-x64/native/Cli.exe
```
