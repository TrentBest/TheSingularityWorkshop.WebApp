using TheSingularityWorkshop.MicroBundleDomain;
using TheSingularityWorkshop.Workshop.Gui;

namespace TheSingularityWorkshop.WebApp.Infrastructure;

public sealed class WebAppMonikerMicroBundle : IMicroBundle
{
    public const ulong BundleId = 3101;
    public const string ProviderId = "workshop-moniker";

    public MicroBundleDescriptor Descriptor { get; } =
        new(BundleId, "1.0.0", providers: [new MicroBundleProvider(ProviderId)]);

    public ulong Id => Descriptor.Id;
    public IReadOnlyList<MicroBundleDependencyRequest> Dependencies => [];
    public GuiNode? Root { get; private set; }

    public void Load(IMicroBundleLoadContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var root = GuiBuilder.Create("Panel", "moniker-root")
            .Property("class", "webapp-moniker flex-hello")
            .Property("aria-label", "The Singularity Workshop");

        root.Child("Panel", "moniker-orbit", orbit =>
            orbit.Property("class", "flex-hello-orbit"));

        root.Child("Panel", "moniker", moniker =>
        {
            moniker.Property("class", "hello-moniker");

            AddLine(moniker, "THE", 0);
            AddLine(moniker, "SINGULARITY", 3);
            AddLine(moniker, "WORKSHOP", 14);
        });

        root.Child("Text", "moniker-state", state =>
            state.Text("STATE: MONIKER")
                .Property("class", "flex-hello-state"));

        Root = root.Build();
    }

    private static void AddLine(
        GuiBuilder parent,
        string word,
        int startIndex)
    {
        parent.Child("Panel", $"moniker-line-{word.ToLowerInvariant()}", line =>
        {
            line.Property("class", "hello-line");

            for (var glyphIndex = 0; glyphIndex < word.Length; glyphIndex++)
            {
                var phaseIndex = startIndex + glyphIndex;
                var colorClass = (phaseIndex % 6) switch
                {
                    0 => "glyph-red",
                    1 => "glyph-orange",
                    2 => "glyph-yellow",
                    3 => "glyph-green",
                    4 => "glyph-blue",
                    _ => "glyph-magenta"
                };

                line.Child(
                    "Text",
                    $"moniker-{word.ToLowerInvariant()}-{glyphIndex}",
                    glyph => glyph
                        .Text(word[glyphIndex].ToString())
                        .Property("class", $"hello-glyph {colorClass}")
                        .Property(
                            "style",
                            $"--phase:{glyphIndex * 27}deg;--phase-index:{phaseIndex};--wave-time:0;"));
            }
        });
    }

    public bool Arbitrate(IMicroBundleArbitrationContext context, int roundIndex)
    {
        ArgumentNullException.ThrowIfNull(context);
        return false;
    }
}
