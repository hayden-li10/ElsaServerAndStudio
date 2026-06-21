
namespace SchedulingEngine.BusinessEngine.Models
{
    public class EngineDataContext
    {
        //public BusinessDate Date { get; init; }
        //public StrategyType Strategy { get; init; }

        //public IReadOnlyList<ShiftBaseline> Baselines { get; }
        //public IReadOnlyList<Depot> Depots { get; }
        //public IReadOnlyList<RoutingArea> RoutingAreas { get; }
        //public IReadOnlyList<TradingHours> TradingHours { get; }
        //public IReadOnlyList<RestBreakRule> RestBreakRules { get; }

        //public IReadOnlyList<Vehicle> Vehicles { get; }
        //public IReadOnlyList<VehicleCostModel> VehicleCosts { get; }

        //public IReadOnlyList<HolidayRule> HolidayRules { get; }
        //public IReadOnlyList<TravelDurationFactor> TravelDurationFactors { get; }

        //public IReadOnlyList<FeatureFlag> FeatureFlags { get; }
        //public IReadOnlyList<OverrideRule> OverrideRules { get; }

        //public IReadOnlyList<ExistingShift> ExistingShifts { get; }
        public IReadOnlyList<int> TestNumbers { get; init; }
        public EngineRunMetadata Metadata { get; init; }


        public EngineDataContext(
            IReadOnlyList<int> testNumbers,
            EngineRunMetadata metadata) 
        {
            TestNumbers = testNumbers;
            Metadata = metadata;
        }

        //public EngineDataContext(
        //    BusinessDate date,
        //    StrategyType strategy,
        //    IReadOnlyList<ShiftBaseline> baselines,
        //    IReadOnlyList<Depot> depots,
        //    IReadOnlyList<RoutingArea> routingAreas,
        //    IReadOnlyList<TradingHours> tradingHours,
        //    IReadOnlyList<RestBreakRule> restBreakRules,
        //    IReadOnlyList<Vehicle> vehicles,
        //    IReadOnlyList<VehicleCostModel> vehicleCosts,
        //    IReadOnlyList<HolidayRule> holidayRules,
        //    IReadOnlyList<TravelDurationFactor> travelDurationFactors,
        //    IReadOnlyList<FeatureFlag> featureFlags,
        //    IReadOnlyList<OverrideRule> overrideRules,
        //    IReadOnlyList<ExistingShift> existingShifts,
        //    EngineRunMetadata metadata)
        //{
        //    Date = date;
        //    Strategy = strategy;
        //    Baselines = baselines;
        //    Depots = depots;
        //    RoutingAreas = routingAreas;
        //    TradingHours = tradingHours;
        //    RestBreakRules = restBreakRules;
        //    Vehicles = vehicles;
        //    VehicleCosts = vehicleCosts;
        //    HolidayRules = holidayRules;
        //    TravelDurationFactors = travelDurationFactors;
        //    FeatureFlags = featureFlags;
        //    OverrideRules = overrideRules;
        //    ExistingShifts = existingShifts;
        //    Metadata = metadata;
        //}
    }
}
