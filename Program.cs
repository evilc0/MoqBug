using System;

namespace MoqBug
{
    public interface IHardware
    {
        void Transmit(IntPtr ptr, byte[] send, byte[] recv);
    }

    public class MyImpl : IHardware
    {
        public void Transmit(IntPtr ptr, byte[] send, byte[] recv)
        {
            Console.WriteLine(ptr);
            Console.WriteLine(BitConverter.ToString(send));
            Console.WriteLine(BitConverter.ToString(recv));
        }
    }

    public class MyModule
    {
        internal IHardware hw = new MyImpl();

        public void DoWork(byte[] send)
        {
            //... here is some complicated stuff to determine the send-buffer

            hw.Transmit((IntPtr)1, send, new byte[8]);
        }
    }
}
