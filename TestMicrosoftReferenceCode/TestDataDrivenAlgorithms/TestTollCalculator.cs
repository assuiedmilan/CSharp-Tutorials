using System;
using System.Collections.Generic;
using MicrosoftReference.DataDrivenAlgorithms.CommercialRegistration;
using MicrosoftReference.DataDrivenAlgorithms.ConsumerVehicleRegistration;
using MicrosoftReference.DataDrivenAlgorithms.LiveryRegistration;
using MicrosoftReference.DataDrivenAlgorithms.Tolls;
using NUnit.Framework;

namespace TestMicrosoftReferenceCode.TestDataDrivenAlgorithms
{
    public class TestTollCalculator
    {
        private readonly TollCalculator _calculator = new();


            public static IEnumerable<TestCaseData> TollsTestCases
            {
                get
                {
                    yield return new TestCaseData(new Car()).Returns(2.50m);
                    yield return new TestCaseData(new Car {Passengers = 1}).Returns(2.00m);
                    yield return new TestCaseData(new Car {Passengers = 2}).Returns(1.50m);
                    yield return new TestCaseData(new Car {Passengers = 3}).Returns(1.00m);
                    yield return new TestCaseData(new Car {Passengers = 4}).Returns(1.00m);

                    yield return new TestCaseData(new Taxi()).Returns(4.50m);
                    yield return new TestCaseData(new Taxi {Fares = 1}).Returns(3.50m);
                    yield return new TestCaseData(new Taxi {Fares = 2}).Returns(3.00m);
                    yield return new TestCaseData(new Taxi {Fares = 3}).Returns(2.50m);
                    yield return new TestCaseData(new Taxi {Fares = 4}).Returns(2.50m);

                    yield return new TestCaseData(new Bus()).Returns(5.00m);
                    yield return new TestCaseData(new Bus {Riders = 99, Capacity = 200}).Returns(7.00m);
                    yield return new TestCaseData(new Bus {Riders = 110, Capacity = 200}).Returns(5.00m);
                    yield return new TestCaseData(new Bus {Riders = 181, Capacity = 200}).Returns(4.00m);

                    yield return new TestCaseData(new DeliveryTruck()).Returns(8.00m);
                    yield return new TestCaseData(new DeliveryTruck {GrossWeightClass = 5001}).Returns(15.00m);
                    yield return new TestCaseData(new DeliveryTruck {GrossWeightClass = 3000}).Returns(10.00m);
                }
            }

            public static IEnumerable<TestCaseData> PremiumPeakCoefficients
            {
                get
                {
                    //Overnight weekday
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 5, 59, 00), true).Returns(0.75m);
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 20, 00, 00), true).Returns(0.75m);

                    yield return new TestCaseData(new DateTime(2021, 3, 1, 5, 59, 00), false).Returns(0.75m);
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 20, 00, 00), false).Returns(0.75m);

                    //Morning rush weekday
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 6, 00, 00), true).Returns(2.00m);
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 9, 59, 00), true).Returns(2.00m);

                    yield return new TestCaseData(new DateTime(2021, 3, 1, 6, 00, 00), false).Returns(1.00m);
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 9, 59, 00), false).Returns(1.00m);

                    //Daytime weekday
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 10, 00, 00), true).Returns(1.50m);
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 15, 59, 00), true).Returns(1.50m);

                    yield return new TestCaseData(new DateTime(2021, 3, 1, 10, 00, 00), false).Returns(1.50m);
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 15, 59, 00), false).Returns(1.50m);

                    //Evening ruh weekday
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 16, 00, 00), true).Returns(1.00m);
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 19, 59, 00), true).Returns(1.00m);

                    yield return new TestCaseData(new DateTime(2021, 3, 1, 16, 00, 00), false).Returns(2.00m);
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 19, 59, 00), false).Returns(2.00m);

                    //Weekend
                    yield return new TestCaseData(new DateTime(2021, 3, 6, 5, 59, 00), true).Returns(1.00m);
                    yield return new TestCaseData(new DateTime(2021, 3, 1, 19, 59, 00), true).Returns(1.00m);

                    yield return new TestCaseData(new DateTime(2021, 3, 6, 5, 59, 00), false).Returns(1.00m);
                    yield return new TestCaseData(new DateTime(2021, 3, 6, 6, 00, 00), false).Returns(1.00m);

                }
            }

        [Test]
        [TestCaseSource(nameof(TollsTestCases))]
        public decimal TestDefaultTolls(object vehicle)
        {
           return _calculator.CalculateToll(vehicle);
        }
        
        [Test]
        [TestCaseSource(nameof(PremiumPeakCoefficients))]
        public decimal TestPremiums(DateTime timeOfToll, bool inbound)
        {
            return _calculator.PeakTimePremiumFull(timeOfToll, inbound);
        }
        
        [Test, Combinatorial]
        /*
         Only one test case would be necessary here, or use mocking to assert a product is made, but wanted to experiment with combinations. 
         It's rather cumbersome since TestCaseSource cannot be combined directly, so this is definitely not a valid use case.
         What is the other tested methods where private ? Should spy ? Simply test final result would not provide information about which computation is wrong (vehicle or premium)
        */
        public void TestTimedToll([ValueSource(nameof(TollsTestCases))] TestCaseData vehicleData, [ValueSource(nameof(PremiumPeakCoefficients))] TestCaseData premiumPeakData)
        {
            var expectedToll = (decimal) vehicleData.ExpectedResult * (decimal) premiumPeakData.ExpectedResult;
            var actualToll = _calculator.CalculateToll(vehicleData.Arguments[0], (DateTime) premiumPeakData.Arguments[0], (bool) premiumPeakData.Arguments[1]);
            
            Assert.AreEqual(expectedToll, actualToll);
        }
        
        [Test]
        public void TestNonExpectedVehiclesThrowException()
        {
            Assert.That(() => _calculator.CalculateToll("ThisIsNotAVehicle"), Throws.TypeOf<ArgumentException>());
        }
        
        [Test]
        public void TestNullVehiclesThrowException()
        {
            Car nonInitializedCar = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.That(() => _calculator.CalculateToll(nonInitializedCar), Throws.TypeOf<ArgumentNullException>());
        }
    }
}