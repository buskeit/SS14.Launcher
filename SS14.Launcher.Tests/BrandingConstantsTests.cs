using System.Collections.Generic;
using NUnit.Framework;

namespace SS14.Launcher.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public sealed class BrandingConstantsTests
{
    [Test]
    public void PrimaryServerDefaultsToLocalTestingPort()
    {
        var address = BrandingConstants.ResolvePrimaryServerAddress(_ => null);

        Assert.That(address, Is.EqualTo("ss14://localhost:1212/"));
    }

    [Test]
    public void FullPrimaryServerAddressOverrideTakesPrecedence()
    {
        var values = new Dictionary<string, string>
        {
            [BrandingConstants.ServerAddressEnvironmentVariable] = " ss14://testing.example.com:4242/ ",
            [BrandingConstants.ServerHostEnvironmentVariable] = "ignored.example.com",
            [BrandingConstants.ServerPortEnvironmentVariable] = "9999"
        };

        var address = BrandingConstants.ResolvePrimaryServerAddress(name => values.GetValueOrDefault(name));

        Assert.That(address, Is.EqualTo("ss14://testing.example.com:4242/"));
    }

    [Test]
    public void HostAndPortCanBeOverridden()
    {
        var values = new Dictionary<string, string>
        {
            [BrandingConstants.ServerHostEnvironmentVariable] = "testing.example.com",
            [BrandingConstants.ServerPortEnvironmentVariable] = "4242"
        };

        var address = BrandingConstants.ResolvePrimaryServerAddress(name => values.GetValueOrDefault(name));

        Assert.That(address, Is.EqualTo("ss14://testing.example.com:4242/"));
    }
}
