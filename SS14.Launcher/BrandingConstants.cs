using System;

namespace SS14.Launcher;

/// <summary>
/// Final Frontier product branding and deployment-specific defaults.
/// </summary>
public static class BrandingConstants
{
    public const string ProductName = "Final Frontier Launcher";
    public const string ShortName = "Final Frontier";
    public const string AppDataName = "FinalFrontierLauncher";
    public const string ForkIdentifier = "final-frontier";

    public const string PrimaryServerName = "The Final Frontier Testing";
    public const string DefaultPrimaryServerHost = "localhost";
    public const ushort DefaultPrimaryServerPort = 1212;

    public const string AppDataEnvironmentVariable = "FINAL_FRONTIER_LAUNCHER_APPDATA_NAME";
    public const string ServerAddressEnvironmentVariable = "FF_LAUNCHER_SERVER_ADDRESS";
    public const string ServerHostEnvironmentVariable = "FF_LAUNCHER_SERVER_HOST";
    public const string ServerPortEnvironmentVariable = "FF_LAUNCHER_SERVER_PORT";

    public const string WikiUrl = "https://thefinalfrontier.miraheze.org/wiki/Main_Page";
    public const string DiscordUrl = "https://discord.gg/mnRU5uX9zu";

    public static string PrimaryServerAddress { get; } = ResolvePrimaryServerAddress(Environment.GetEnvironmentVariable);

    internal static string ResolvePrimaryServerAddress(Func<string, string?> getEnvironmentVariable)
    {
        var addressOverride = getEnvironmentVariable(ServerAddressEnvironmentVariable);
        if (!string.IsNullOrWhiteSpace(addressOverride))
            return addressOverride.Trim();

        var host = getEnvironmentVariable(ServerHostEnvironmentVariable);
        if (string.IsNullOrWhiteSpace(host))
            host = DefaultPrimaryServerHost;

        var port = DefaultPrimaryServerPort;
        var portOverride = getEnvironmentVariable(ServerPortEnvironmentVariable);
        if (!string.IsNullOrWhiteSpace(portOverride) && ushort.TryParse(portOverride, out var parsedPort))
            port = parsedPort;

        return new UriBuilder("ss14", host.Trim(), port).Uri.ToString();
    }
}
