using TheSingularityWorkshop.WebApp.Infrastructure;
using Xunit;

namespace TheSingularityWorkshop.WebApp.Tests;

public sealed class WebAppCompositionTests
{
    [Fact]
    public void Startup_runtime_composes_the_moniker_through_fsm_cos()
    {
        var runtime = new WebAppCosRuntime(new WebAppMicroBundleCatalog());

        Assert.Equal(WebAppCosRuntime.RuntimeId, runtime.Assembly.RuntimeId);
        Assert.Single(runtime.Assembly.Bundles);
        Assert.True(runtime.Assembly.TryGetBundle<WebAppMonikerMicroBundle>(
            WebAppMonikerMicroBundle.BundleId,
            out var moniker));
        Assert.Same(runtime.Moniker, moniker);
        Assert.NotNull(moniker!.Root);
    }

    [Fact]
    public void Moniker_contains_the_three_expected_word_rows()
    {
        var runtime = new WebAppCosRuntime(new WebAppMicroBundleCatalog());

        var root = runtime.Moniker.Root!;
        var column = root.Find("moniker-column");

        Assert.Equal(3, column.Children.Count);
        Assert.Equal("moniker-the", column.Children[0].Id);
        Assert.Equal("moniker-singularity", column.Children[1].Id);
        Assert.Equal("moniker-workshop", column.Children[2].Id);
    }
}



    [Fact]
    public void Experience_preallocates_independent_living_actor_contexts()
    {
        using var experience = new WebAppExperienceRuntime();

        Assert.Equal(100, experience.Actors.Count);
        Assert.All(experience.Actors, actor => Assert.False(actor.Active));
        Assert.Equal("Gateway", experience.CurrentState);
    }

    [Fact]
    public void Enter_starts_the_fsm_driven_living_experience()
    {
        using var experience = new WebAppExperienceRuntime();

        experience.RequestEnter();

        Assert.True(experience.EnterRequested);
        Assert.True(experience.Actors[0].Active);
        Assert.True(experience.Actors[0].IsRoot);
        Assert.Equal(1, experience.Population);
    }
