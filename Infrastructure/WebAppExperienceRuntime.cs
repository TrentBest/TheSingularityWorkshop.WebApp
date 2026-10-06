using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TheSingularityWorkshop.FSM_API;
using fsm_API = TheSingularityWorkshop.FSM_API.FSM_API;

namespace TheSingularityWorkshop.WebApp.Infrastructure;

/// <summary>
/// The WebApp manifestation of the Workshop landing Experience.
/// The browser is presentation; FSM_API owns progression and every living actor.
/// All actors share one processing group but retain independent FSM instances.
/// </summary>
public sealed class WebAppExperienceRuntime : IDisposable
{
    private const int PopulationTarget = 100;
    private const double SeedSize = 10;
    private const double MatureSize = 50;
    private const double TravelLerp = 0.16;
    private const double GrowthLerp = 0.22;
    private const double GravityAcceleration = 0.85;
    private const int MonikerTicks = 90;

    private readonly string _pageGroup = $"WebApp:Page:{Guid.NewGuid():N}";
    private readonly string _actorGroup = $"WebApp:LivingGui:{Guid.NewGuid():N}";
    private readonly List<ActorContext> _actors = new(PopulationTarget);
    private readonly CancellationTokenSource _shutdown = new();
    private readonly Thread _heartbeat;
    private readonly object _sync = new();

    private PageContext _page = null!;
    private bool _disposed;

    public WebAppExperienceRuntime()
    {
        fsm_API.Create.CreateProcessingGroup(_pageGroup);
        fsm_API.Create.CreateProcessingGroup(_actorGroup);

        fsm_API.Create.CreateFiniteStateMachine("WebAppPageFSM", -1, _pageGroup)
            .State("Gateway", onEnter: null, onUpdate: _ => { }, onExit: null)
            .State("LivingGui", onEnter: _ => ActivateRoot(), onUpdate: _ => TickLivingGui(), onExit: null)
            .State("Moniker", onEnter: _ => _page.MonikerVisible = true, onUpdate: _ => TickMoniker(), onExit: null)
            .State("Gravity", onEnter: _ => FreezePopulation(), onUpdate: _ => TickGravity(), onExit: null)
            .State("Running", onEnter: _ => _page.NavigationVisible = true, onUpdate: _ => { }, onExit: null)
            .WithInitialState("Gateway")
            .Transition("Gateway", "LivingGui", c => ((PageContext)c).EnterRequested)
            .Transition("LivingGui", "Moniker", c => ((PageContext)c).Population >= PopulationTarget)
            .Transition("Moniker", "Gravity", c => ((PageContext)c).StateTicks >= MonikerTicks)
            .Transition("Gravity", "Running", c => ((PageContext)c).Fallen)
            .BuildDefinition();

        fsm_API.Create.CreateFiniteStateMachine("WebAppLivingActorFSM", -1, _actorGroup)
            .State("Alive", onEnter: null, onUpdate: c => AdvanceActor((ActorContext)c), onExit: null)
            .WithInitialState("Alive")
            .BuildDefinition();

        _page = new PageContext();

        for (var index = 0; index < PopulationTarget; index++)
        {
            var actor = new ActorContext();
            _actors.Add(actor);
            fsm_API.Create.CreateInstance("WebAppLivingActorFSM", actor, _actorGroup);
        }

        _heartbeat = new Thread(Heartbeat)
        {
            IsBackground = true,
            Name = "WebApp.ExperienceHeartbeat"
        };
        _heartbeat.Start();
    }

    public event Action? Changed;

    public IReadOnlyList<ActorContext> Actors => _actors;
    public string CurrentState => GetPageState();
    public bool EnterRequested => _page.EnterRequested;
    public bool MonikerVisible => _page.MonikerVisible;
    public bool NavigationVisible => _page.NavigationVisible;
    public int Population => _page.Population;
    public long Ticks => _page.TotalTicks;

    public void RequestEnter()
    {
        lock (_sync)
        {
            if (_disposed || _page.EnterRequested)
                return;

            _page.EnterRequested = true;
        }

        fsm_API.Interaction.Update(_pageGroup);
        Changed?.Invoke();
    }

    private string GetPageState()
    {
        try
        {
            return fsm_API.Interaction.GetInstances("WebAppPageFSM", _pageGroup).FirstOrDefault()?.CurrentState
                ?? "Gateway";
        }
        catch
        {
            return "Gateway";
        }
    }

    private void ActivateRoot()
    {
        var root = _actors[0];
        if (root.Active)
            return;

        root.Activate(0, _page.TotalTicks);
        _page.Population = 1;
    }

    private void TickLivingGui()
    {
        if (_page.Frozen)
            return;

        fsm_API.Interaction.Update(_actorGroup);

        var population = 0;
        foreach (var actor in _actors)
        {
            if (actor.Active)
                population++;
        }

        _page.Population = population;
    }

    private void TickMoniker() => _page.StateTicks++;

    private void FreezePopulation()
    {
        _page.Frozen = true;
        _page.StateTicks = 0;
    }

    private void TickGravity()
    {
        _page.StateTicks++;

        foreach (var actor in _actors)
        {
            if (!actor.Active)
                continue;

            actor.Y += actor.GravityVelocity;
            actor.GravityVelocity += GravityAcceleration;
            actor.Rotation += 2.4;
        }

        _page.Fallen = _actors.Count == PopulationTarget &&
                       _actors.Where(a => a.Active).All(a => a.Y > 125);
    }

    private void AdvanceActor(ActorContext actor)
    {
        if (!actor.Active || _page.Frozen)
            return;

        switch (actor.Phase)
        {
            case ActorPhase.Growing:
                actor.Size += (MatureSize - actor.Size) * GrowthLerp;
                if (Math.Abs(actor.Size - MatureSize) <= 0.5)
                {
                    actor.Size = MatureSize;
                    actor.Phase = ActorPhase.Traveling;
                }
                break;

            case ActorPhase.Traveling:
                actor.X += (actor.TargetX - actor.X) * TravelLerp;
                actor.Y += (actor.TargetY - actor.Y) * TravelLerp;
                actor.Rotation += 24;

                if (Math.Abs(actor.X - actor.TargetX) < 0.35 &&
                    Math.Abs(actor.Y - actor.TargetY) < 0.35)
                {
                    actor.X = actor.TargetX;
                    actor.Y = actor.TargetY;
                    actor.Phase = ActorPhase.Existing;
                }
                break;

            case ActorPhase.Existing:
                actor.Size += (MatureSize - actor.Size) * GrowthLerp;
                actor.Phase = ActorPhase.Existing;
                break;
        }

        if (actor.IsRoot && actor.Phase == ActorPhase.Existing)
            ReproduceRoot();
    }

    private void ReproduceRoot()
    {
        var next = _actors.FirstOrDefault(a => !a.Active);
        if (next is null)
            return;

        next.Activate(
            _actors.Count(a => a.Active),
            _page.TotalTicks);
        _page.Population = _actors.Count(a => a.Active);
    }

    private void Heartbeat()
    {
        while (!_shutdown.IsCancellationRequested)
        {
            try
            {
                lock (_sync)
                {
                    if (!_disposed)
                    {
                        _page.TotalTicks++;
                        fsm_API.Interaction.Update(_pageGroup);
                    }
                }

                Changed?.Invoke();
                Thread.Sleep(33);
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
            catch
            {
                Thread.Sleep(33);
            }
        }
    }

    public void Dispose()
    {
        lock (_sync)
        {
            if (_disposed)
                return;

            _disposed = true;
            _shutdown.Cancel();
        }

        try { _heartbeat.Interrupt(); } catch { }

        fsm_API.Interaction.DestroyFiniteStateMachine("WebAppLivingActorFSM", _actorGroup);
        fsm_API.Interaction.DestroyFiniteStateMachine("WebAppPageFSM", _pageGroup);
        _shutdown.Dispose();
    }

    public enum ActorPhase
    {
        Dormant,
        Growing,
        Traveling,
        Existing
    }

    public sealed class ActorContext : IStateContext
    {
        internal ActorContext()
        {
            Name = "WebAppLivingActor";
            IsValid = true;
        }

        public string Name { get; set; }
        public bool IsValid { get; set; }
        public bool Active { get; private set; }
        public bool IsRoot { get; private set; }
        public int Generation { get; private set; }
        public long BirthTick { get; private set; }
        public string Lineage { get; private set; } = string.Empty;
        public double X { get; internal set; }
        public double Y { get; internal set; }
        public double TargetX { get; internal set; }
        public double TargetY { get; internal set; }
        public double Size { get; internal set; } = SeedSize;
        public double Rotation { get; internal set; }
        public double GravityVelocity { get; internal set; }
        public ActorPhase Phase { get; internal set; } = ActorPhase.Dormant;

        internal void Activate(int population, long birthTick)
        {
            Active = true;
            IsRoot = population == 0;
            Generation = IsRoot ? 0 : 1;
            BirthTick = birthTick;
            Lineage = IsRoot ? "G0" : $"G{population - 1}";
            X = 50;
            Y = 50;
            Size = IsRoot ? 50 : SeedSize;
            Rotation = Squirrel3(population, 0x524F5445u) % 360;
            GravityVelocity = 0;
            Phase = IsRoot ? ActorPhase.Existing : ActorPhase.Growing;

            if (IsRoot)
            {
                TargetX = 50;
                TargetY = 50;
            }
            else
            {
                var slot = population;
                var column = slot % 10;
                var row = slot / 10;
                var jitterX = ToUnit(Squirrel3((slot * 2) + 0, 0x50414745u)) - 0.5;
                var jitterY = ToUnit(Squirrel3((slot * 2) + 1, 0x50414745u)) - 0.5;
                TargetX = 10 + ((column + 0.5 + (jitterX * 0.30)) * 8);
                TargetY = 10 + ((row + 0.5 + (jitterY * 0.30)) * 8);
            }
        }

        private static double ToUnit(uint value) => value / 4294967296d;

        private static uint Squirrel3(int position, uint seed)
        {
            unchecked
            {
                var mangled = (uint)position;
                mangled *= 0x68E31DA4u;
                mangled += seed;
                mangled ^= mangled >> 8;
                mangled += 0xB5297A4Du;
                mangled ^= mangled << 8;
                mangled *= 0x1B56C4E9u;
                mangled ^= mangled >> 8;
                return mangled;
            }
        }
    }

    public sealed class PageContext : IStateContext
    {
        public string Name { get; set; } = "WebAppPage";
        public bool IsValid { get; set; } = true;
        public bool EnterRequested { get; set; }
        public bool MonikerVisible { get; set; }
        public bool NavigationVisible { get; set; }
        public bool Frozen { get; set; }
        public bool Fallen { get; set; }
        public int Population { get; set; }
        public long TotalTicks { get; set; }
        public long StateTicks { get; set; }
    }
}
