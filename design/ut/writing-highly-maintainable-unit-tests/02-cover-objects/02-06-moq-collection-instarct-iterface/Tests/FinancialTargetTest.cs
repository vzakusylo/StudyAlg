using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace moq_collections_instarct_iterface
{
    [TestClass]
    public class FinancialTargetTest
    {
        [TestMethod]
        public void InitializePoints_ResutlsStartsWithValue3()
        {
            MyArray array = new FinancialTarget().InitializePoints();
            Assert.AreEqual(3, array.GetElementAt(0));
        }

        [TestMethod]
        public void AddTargetPoints_Count2_ArrayAppendCalledTwoTimes()
        {
            FinancialTarget target = new FinancialTarget();
            Mock<IMyArray> listMock = new Mock<IMyArray>();

            int[] expectedPoints = { 3, 5 };
            int callIndex = 0;
            bool sequenceFine = true;

            listMock
                .Setup(list => list.Append(It.IsAny<int>()))
                .Callback((int x) =>
                {
                    sequenceFine = sequenceFine && x == expectedPoints[callIndex++];
                });

            target.AddTargetPoints(listMock.Object, 2);

            Assert.IsTrue(callIndex > 0);
            Assert.IsTrue(sequenceFine);
        }
    }
}
