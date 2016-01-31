using System;
using System.Collections.Generic;

namespace PASM
{
    public class Memory
    {
        private List<Part> FreeParts = new List<Part>();
        private List<Part> UsedParts = new List<Part>();
        public Part[] PartAddressStack;
        public byte[] Data;
        public int DataLength => Data.Length;

        public Memory(int size)
        {
            Data = new byte[size];
            PartAddressStack = new Part[size];
            Part StartBlock = new Part(0, size);
            FreeParts.Add(StartBlock);
            PartAddressStack[0] = StartBlock;
        }

        public int Allocate(int size)
        {
            Part AllocatedPart = getFreeBlock(size);
            if (AllocatedPart.Size > size)
            {
                Part pt = new Part(AllocatedPart.Address + size, AllocatedPart.Size - size);
                FreeParts.Insert(0, pt);
                PartAddressStack[pt.Address] = pt;
                AllocatedPart.Size = size;
            }
            AllocatedPart.Used = true;
            FreeParts.Remove(AllocatedPart);
            PartAddressStack[AllocatedPart.Address] = AllocatedPart;

            UsedParts.Insert(0, AllocatedPart);
            return AllocatedPart.Address;
        }

        private Part getFreeBlock(int requiredSize)
        {
            foreach (Part p in FreeParts)
                if (p.Size >= requiredSize)
                    return p;
            throw new MemoryException("Unable to allocate " + requiredSize + " bytes of memory to RAM, Out of memory?");
        }

        public void Free(int Address)
        {
            Part p = PartAddressStack[Address];
            p.Used = false;
            UsedParts.Remove(p);
            FreeParts.Insert(0, p);
            Stitch(Address);
        }

        private void Stitch(int currentAddress)
        {
            Part currentBlock = PartAddressStack[currentAddress];
            int nextAddress = NextBlock(currentAddress);
            Part nextBlock = PartAddressStack[nextAddress];
            if (!nextBlock.Used)
            {
                currentBlock.Size += nextBlock.Size;
                FreeParts.Remove(nextBlock);
                PartAddressStack[nextAddress] = null;
            }
            if (currentAddress != 0)
            {
                int previousAddress = PreviousBlock(currentAddress);
                Part previousBlock = PartAddressStack[previousAddress];
                if (!previousBlock.Used)
                {
                    previousBlock.Size += currentBlock.Size;
                    FreeParts.Remove(currentBlock);
                    PartAddressStack[currentAddress] = null;
                }
            }
        }

        private int NextBlock(int currentAddress)
        {
            //return currentAddress + size?
            while (true)
            {
                currentAddress++;
                if (PartAddressStack[currentAddress] != null) return currentAddress;
            }
        }


        //Get the previous block location from another block
        //TODO: Make this faster!!
        private int PreviousBlock(int currentAddress)
        {
            while (true)
            {
                currentAddress--;
                if (PartAddressStack[currentAddress] != null) return currentAddress;
            }
        }

        //Writes & reads
        public void write(byte[] data, int address)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Data[address + i] = data[i];
            }
        }

        //Read the data from a given address.
        public byte[] read(int address, int size)
        {
            byte[] data = new byte[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = Data[address + i];
            }
            return data;
        }
        //Write the data at the address given to 0 (null)
        public void clean(int address, int size)
        {
            write(new byte[size], address);
        }

        public class Part
        {
            public bool Used;
            public int Address, Size;
            public Part(int Address, int Size)
            {
                this.Address = Address;
                this.Size = Size;
            }
        }

        private class MemoryException : Exception
        {
            public MemoryException(string exception) : base(exception) { }
        }
    }
}
