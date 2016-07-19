using System.Collections.Generic;

namespace PASM
{
	
    /// <summary>
    /// Virtual memory container for the PASM engine to allocate data to, and also grab data from an address.
    /// </summary>
    public class Memory
    {
        private List<Part> FreeParts = new List<Part>();
        private List<Part> UsedParts = new List<Part>();
		public Part[] PartAddressStack;
        public byte[] Data;
        public uint DataLength;
        
        public Memory(uint size)
        {
            Data = new byte[size];
            PartAddressStack = new Part[size];
            Part StartBlock = new Part(0, size);
            FreeParts.Add(StartBlock);
            DataLength = size;
            PartAddressStack[0] = StartBlock;
        }

		/// <summary>
		/// Allocate the specified size.
		/// </summary>
		/// <param name="size">Size.</param>
        public uint Allocate(uint size)
        {
            Part AllocatedPart = getFreeBlock(size);
            if (AllocatedPart.Size > size)
            {
                Part pt = new Part(AllocatedPart.Address + size, AllocatedPart.Size - size);
                FreeParts.Insert(0, pt);
                PartAddressStack[pt.Address] = pt;
                //Check if it is possible for another address to be above it, if so, set the part above's BackAddress.
				if (pt.Address+pt.Size < PartAddressStack.Length)
                	PartAddressStack[pt.Address+pt.Size].BackAddress = pt.Size;
                AllocatedPart.Size = size;
            }
            AllocatedPart.Used = true;
            FreeParts.Remove(AllocatedPart);
            PartAddressStack[AllocatedPart.Address] = AllocatedPart;

            UsedParts.Insert(0, AllocatedPart);
            return AllocatedPart.Address;
        }

		/// <summary>
		/// Gets the free block.
		/// </summary>
		/// <returns>The free block.</returns>
		/// <param name="requiredSize">Required size.</param>
        private Part getFreeBlock(uint requiredSize)
        {
            foreach (Part p in FreeParts)
                if (p.Size >= requiredSize)
                    return p;
            throw new MemoryException("Unable to allocate " + requiredSize + " bytes of memory to RAM, Out of memory?");
        }

		/// <summary>
		/// Free the specified Address.
		/// </summary>
		/// <param name="Address">Address.</param>
        public void Free(uint Address)
        {
            Part p = PartAddressStack[Address];
            p.Used = false;
            UsedParts.Remove(p);
            FreeParts.Insert(0, p);
            Stitch(Address);
        }

		/// <summary>
		/// Stitch the specified currentAddress to it's backAddress.
		/// </summary>
		/// <param name="currentAddress">Current address.</param>
        private void Stitch(uint currentAddress)
        {
            Part currentBlock = PartAddressStack[currentAddress];
            uint nextAddress = NextBlock(currentAddress);
            Part nextBlock = PartAddressStack[nextAddress];
            if (!nextBlock.Used)
            {
                currentBlock.Size += nextBlock.Size;
                FreeParts.Remove(nextBlock);
                PartAddressStack[nextAddress] = null;
                if (currentAddress + currentBlock.Size < PartAddressStack.Length)
                    PartAddressStack[currentAddress + currentBlock.Size].BackAddress = currentBlock.Address;
            }
            if (currentAddress != 0)
            {
                uint previousAddress = PreviousBlock(currentAddress);
                Part previousBlock = PartAddressStack[previousAddress];
                if (!previousBlock.Used)
                {
                    previousBlock.Size += currentBlock.Size;
                    FreeParts.Remove(currentBlock);
                    PartAddressStack[currentAddress] = null;
                    if (currentAddress + currentBlock.Size < PartAddressStack.Length)
                        PartAddressStack[nextAddress].BackAddress = previousAddress;
                }
            }
        }


		/// <summary>
		/// Gets the next block from the given address.
		/// </summary>
		/// <returns>The block.</returns>
		/// <param name="currentAddress">Current address.</param>
        private uint NextBlock(uint currentAddress) => PartAddressStack[PartAddressStack[currentAddress].getNextAddress()].Address;

		/// <summary>
		/// Gets the previous block address from a given address.
		/// </summary>
		/// <returns>The block.</returns>
		/// <param name="currentAddress">Current address.</param>
        private uint PreviousBlock(uint currentAddress)
        {
            return PartAddressStack[currentAddress].BackAddress;
        }

		/// <summary>
		/// Write the specified data and address.
		/// </summary>
		/// <param name="data">Data.</param>
		/// <param name="address">Address.</param>
        public void write(byte[] data, uint address)
        {
            for (uint i = 0; i < data.Length; i++)
            {
                Data[address + i] = data[i];
            }
        }

		/// <summary>
		/// Read the specified address and size.
		/// </summary>
		/// <param name="address">Address.</param>
		/// <param name="size">Size.</param>
        public byte[] read(uint address, uint size)
        {
            byte[] data = new byte[size];
            for (uint i = 0; i < size; i++)
            {
                data[i] = Data[address + i];
            }
            return data;
        }

        //Write the data at the address given to 0 (null)
        public void clean(uint address, uint size)
        {
            write(new byte[size], address);
        }

        public class Part
        {
            //13 Bytes per part
            public uint BackAddress;
            public bool Used;
            public uint Address, Size;
            
            public uint getNextAddress() => Address + Size;
            public Part(uint Address, uint Size)
            {
                this.Address = Address;
                this.Size = Size;
            }
        }

		/// <summary>
		/// Memory extension extends PException (Required for debugging)
		/// </summary>
        private class MemoryException : PException
        {
            public MemoryException(string exception) : base(exception) { }
        }
    }
}
