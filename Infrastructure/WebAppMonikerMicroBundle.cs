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
            .Property("background", "#020711")
            .Property("display", "flex")
            .Property("justifyContent", "center")
            .Property("alignItems", "center");

        root.Child(
            "Panel",
            "moniker-column",
            column =>
            {
                column.Property("display", "flex")
                    .Property("flexDirection", "column")
                    .Property("alignItems", "center")
                    .Property("padding", "24");

                foreach (var (word, offset) in new[] { ("THE", 0), ("SINGULARITY", 3), ("WORKSHOP", 14) })
                {
                    column.Child(
                        "Panel",
                        $"moniker-{word.ToLowerInvariant()}",
                        row =>
                        {
                            row.Property("display", "flex")
                                .Property("justifyContent", "center");

                            for (var index = 0; index < word.Length; index++)
                            {
                                var phase = (offset + index) % 6;
                                var foreground = phase switch
                                {
                                    0 => "#FF3030",
                                    1 => "#FF7A00",
                                    2 => "#FFD34D",
                                    3 => "#52E05A",
                                    4 => "#00A8FF",
                                    _ => "#FF2CFF"
                                };

                                row.Child(
                                    "Text",
                                    $"moniker-{word.ToLowerInvariant()}-{index}",
                                    character => character
                                        .Text(word[index].ToString())
                                        .Property("foreground", foreground)
                                        .Property("fontSize", "76")
                                        .Property("fontWeight", "700")
                                        .Property("fontFamily", "Consolas"));
                            }
                        });
                }
            });

        Root = root.Build();
    }

    public bool Arbitrate(IMicroBundleArbitrationContext context, int roundIndex)
    {
        ArgumentNullException.ThrowIfNull(context);
        return false;
    }
}
