# Final Frontier Launcher

Final Frontier Launcher is the branded launcher for **The Final Frontier Testing**. It is based on the upstream
`space-wizards/SS14.Launcher` codebase and preserves its content download, build selection, account management,
authentication, and client launch flow.

Community links:

* Wiki/support: https://thefinalfrontier.miraheze.org/wiki/Main_Page
* Discord: https://discord.gg/mnRU5uX9zu

## Local development

Requirements: .NET 10 SDK.

Build the launcher:

```powershell
dotnet restore .\SS14.Launcher.sln
dotnet build .\SS14.Launcher.sln -c Debug --no-restore
```

Run with a development-only appdata/cache name so the run is isolated from both the normal SS14 launcher and a
release Final Frontier installation:

```powershell
$env:FINAL_FRONTIER_LAUNCHER_APPDATA_NAME = "FinalFrontierLauncher-Dev"
dotnet run --project .\SS14.Launcher\SS14.Launcher.csproj -c Debug --no-build
```

Without an override, Final Frontier Launcher uses `FinalFrontierLauncher` under the platform user-data and local-cache
roots. The upstream `SS14_LAUNCHER_APPDATA_NAME` variable remains supported for developer compatibility.

## Testing server

The testing server defaults are defined in `SS14.Launcher/BrandingConstants.cs`:

* host: `localhost`
* port: `1212`
* launcher URI: `ss14://localhost:1212/`

For a local or deployed test target, override either the complete address or the host and port before starting the
launcher:

```powershell
$env:FF_LAUNCHER_SERVER_ADDRESS = "ss14://testing.example.com:1212/"
# Alternatively:
$env:FF_LAUNCHER_SERVER_HOST = "testing.example.com"
$env:FF_LAUNCHER_SERVER_PORT = "1212"
```

To test a connection, start the target server, run the launcher with an isolated appdata name, log in using the normal
SS14 account flow, confirm **The Final Frontier Testing** reports online on Home, and press **Connect**. Status, build
selection, download/update, launch, failure, and client-exit events are written to the launcher log directory shown in
Options.

`SS14_LAUNCHER_OVERRIDE_AUTH=https://.../` can still override the auth API in Debug builds when testing a local auth
service.
