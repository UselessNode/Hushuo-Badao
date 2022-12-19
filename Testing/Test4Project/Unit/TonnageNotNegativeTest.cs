namespace Test4Project.Unit
{
    [TestClass]
    public class TonnageNotNegativeTest
    {
        [TestMethod]
        [DataRow(-1234)]
        [DataRow(0)]
        [DataRow(1000)]
        [DataRow(300d)]
        [DataRow(400f)]
        [DataRow(4000.55)]
        public void TonnageNotNegative(double value) =>
            Assert.IsTrue(value > 0, $"{value} больше 0");

    }
}