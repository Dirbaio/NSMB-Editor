
namespace NSMBe4.DSFileSystem
{
    public class Arm9BinSection
    {
        public byte[] data;
        public int len;
        public int ramAddr;
        public int bssSize;

        public Arm9BinSection(byte[] data, int ramAddr, int bssSize)
        {
            this.data = data;
            len = data.Length;
            this.ramAddr = ramAddr;
            this.bssSize = bssSize;
        }

        public bool containsRamAddr(int addr)
        {
            return addr >= ramAddr && addr < ramAddr + len;
        }

        public uint readFromRamAddr(int addr)
        {
            addr -= ramAddr;

            return (uint)(data[addr] |
                data[addr + 1] << 8 |
                data[addr + 2] << 16 |
                data[addr + 3] << 24);
        }

        public void writeToRamAddr(int addr, uint val)
        {
            addr -= ramAddr;

            data[addr] = (byte)val;
            data[addr + 1] = (byte)(val >> 8);
            data[addr + 2] = (byte)(val >> 16);
            data[addr + 3] = (byte)(val >> 24);
        }
    }
}
