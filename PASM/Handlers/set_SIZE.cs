using static PASM.Extended;
using PASM;
namespace PASM.Handlers {
    public class set_SIZE : Handler {
        
        bool setter_isMethodPtr;
        int setter_Ptr;
        
        
        bool cpy_isMethodPtr;
        
        int cpy_Ptr;
        
        public set_SIZE(string[] args, Engine engine) : base (engine) {
            setter_isMethodPtr = isMethodPointer(args[1], out setter_Ptr);
            
            cpy_isMethodPtr = isMethodPointer(args[3], out cpy_Ptr);
        }
        
        override public void Execute() {
            Register cpy_register = inst.GetRaster(cpy_isMethodPtr)[cpy_Ptr];
            inst.Set(setter_Ptr, setter_isMethodPtr, cpy_register.size);
        }
    }
}