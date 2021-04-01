using System;
using MicrosoftReference.DataDrivenAlgorithms.CommercialRegistration;
using MicrosoftReference.DataDrivenAlgorithms.ConsumerVehicleRegistration;
using MicrosoftReference.DataDrivenAlgorithms.LiveryRegistration;

namespace MicrosoftReference.DataDrivenAlgorithms
{
    namespace Tolls
    {
        public class TollCalculator
        {
            private enum TimeBand
            {
                MorningRush,
                Daytime,
                EveningRush,
                Overnight
            }
            
            private static bool IsWeekDay(DateTime timeOfToll) =>
                timeOfToll.DayOfWeek switch
                {
                    DayOfWeek.Saturday => false,
                    DayOfWeek.Sunday => false,
                    _ => true
                };
            
            private static TimeBand GetTimeBand(DateTime timeOfToll) =>
                timeOfToll.Hour switch
                {
                    < 6 or > 19 => TimeBand.Overnight,
                    < 10 => TimeBand.MorningRush,
                    < 16 => TimeBand.Daytime,
                    _ => TimeBand.EveningRush,
                };
            
            public decimal PeakTimePremiumFull(DateTime timeOfToll, bool inbound) =>
                (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
                {
                    (true, TimeBand.MorningRush, true) => 2.00m,
                    (true, TimeBand.Daytime, _)   => 1.5m,
                    (true, TimeBand.EveningRush, false) => 2.00m,
                    (true, TimeBand.Overnight, _) => 0.75m,
                    _ => 1.0m,
                };

            public decimal CalculateToll(object vehicle, DateTime timeOfToll, bool inbound) => CalculateToll(vehicle) * PeakTimePremiumFull(timeOfToll, inbound);
            
            public decimal CalculateToll(object vehicle) =>
                vehicle switch
                {
                    Car c => c.Passengers switch
                    {
                        0 => 2.00m + 0.5m,
                        1 => 2.0m,
                        2 => 2.0m - 0.5m,
                        _ => 2.00m - 1.0m
                    },

                    Taxi t => t.Fares switch
                    {
                        0 => 3.50m + 1.00m,
                        1 => 3.50m,
                        2 => 3.50m - 0.50m,
                        _ => 3.50m - 1.00m
                    },
                    
                    Bus b when (double)b.Riders / (double)b.Capacity < 0.50 => 5.00m + 2.00m,
                    Bus b when (double)b.Riders / (double)b.Capacity > 0.90 => 5.00m - 1.00m,
                    Bus => 5.00m,
                    
                    DeliveryTruck {GrossWeightClass: > 5000} => 10.00m + 5.00m,
                    DeliveryTruck {GrossWeightClass: < 3000} => 10.00m - 2.00m,
                    DeliveryTruck => 10.00m,
                    
                    { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
                    null=> throw new ArgumentNullException(nameof(vehicle))
                };
        }
    }
}