using System;
using Moq;
using NUnit.Framework;

namespace MoqBug
{
    public class Tests
    {
        private MyModule _sut;

        [Test]
        public void MyTest_Works()
        {
            _sut = new MyModule();

            Mock<IHardware> hwMock = new Mock<IHardware>();
            hwMock.Setup(m => m.Transmit(It.IsAny<IntPtr>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()));
            _sut.hw = hwMock.Object;

            _sut.DoWork(new byte[]{1,2,3});

            hwMock.Verify(m => m.Transmit((IntPtr)1, new byte[]{1,2,3}, It.IsAny<byte[]>()));
        }

        [Test]
        public void MyTest_Works_Also()
        {
            _sut = new MyModule();

            Mock<IHardware> hwMock = new Mock<IHardware>();
            hwMock.Setup(m => m.Transmit(It.IsAny<IntPtr>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()));
            _sut.hw = hwMock.Object;

            _sut.DoWork(new byte[] { 1, 2, 3 });

            string inputAsString = Convert.ToBase64String(new byte[] { 1, 2, 3 });

            var testeeAsByte = Convert.FromBase64String(inputAsString);

            hwMock.Verify(m => m.Transmit((IntPtr)1, testeeAsByte, It.IsAny<byte[]>()));
        }

        [Test]
        public void MyTest_Works_Not()
        {
            _sut = new MyModule();

            Mock<IHardware> hwMock = new Mock<IHardware>();
            hwMock.Setup(m => m.Transmit(It.IsAny<IntPtr>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()));
            _sut.hw = hwMock.Object;

            _sut.DoWork(new byte[] { 1, 2, 3 });

            string inputAsString = Convert.ToBase64String(new byte[] { 1, 2, 3 });

            hwMock.Verify(m => m.Transmit((IntPtr)1, Convert.FromBase64String(inputAsString), It.IsAny<byte[]>()));
        }
    }
}