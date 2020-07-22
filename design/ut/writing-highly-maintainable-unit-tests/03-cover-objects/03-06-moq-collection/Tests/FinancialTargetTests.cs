using _03_moq_collections;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace writing_highly_maintainable_unit_tests._03_cover_objects._03_06_moq_collection.Tests
{
    public class FinancialTargetTests
    {
        [Xunit.Theory]
        [InlineData(-17)]
        [InlineData(-1)]
        public void AddTargetPoints_Throws(int count)
        {
            FinancialTarget target = new FinancialTarget();
            IMyArray list = new Mock<IMyArray>().Object;

            Assert.Throws<ArgumentException>(() => target.AddTargetPoints(list, count));
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void AddTargetPoints_AddSpecifiedNumberOfElement(int count)
        {
            FinancialTarget target = new FinancialTarget();
            Mock<IMyArray> list = new Mock<IMyArray>();
            list.Setup(l=>l.Append(It.IsAny<int>()));

            target.AddTargetPoints(list.Object, count);

            list.Verify(l => l.Append(It.IsAny<int>()), Times.Exactly(count));
        }

        [Xunit.Theory]
        [InlineData(216, 0, 3)]
        [InlineData(2016, 9, 21)]
        [InlineData(2016, 71, 145)]
        [InlineData(2016, 71, 433)]
        public void AddTargetPoints_SpecificValueAtPosition(
            int count, int index, int expectedValue)
        {
            FinancialTarget target = new FinancialTarget();

            Mock<IMyArray> list = new Mock<IMyArray>();
            int addedCount = 0;
            int? valueAtPosition = null;

            list.Setup(l => l.Append(It.IsAny<int>())).Callback<int>(value => 
            {
                if (addedCount++ == index)
                {
                    valueAtPosition = value;
                }
            });

            target.AddTargetPoints(list.Object, count);

            Assert.AreEqual(expectedValue, (int) valueAtPosition);
        }
    }
}
