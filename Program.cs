using Mono.Cecil;
using Mono.Cecil.Cil;
using System;

namespace DynamicILEdit
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = "mystartDir";

            ModuleDefinition module = ModuleDefinition.ReadModule(dir + "\\ClassLibrary1.dll");
            foreach (TypeDefinition type in module.Types)
            { 
                foreach (var meth in type.Methods) {
                    var name = meth.Name;
                    if (name == "Main")
                    {
                        //1: LDSTR
                        var body = meth.Body;
                        var processor = body.GetILProcessor();
                        var createInstruction = processor.Create(OpCodes.Ldstr, "Hello World");
                        processor.Replace(body.Instructions[1], createInstruction);
                    }
                }
                Console.WriteLine(type.FullName);
            }
            
            module.Write(dir + "\\ClassLibrary2.dll");
            Console.ReadKey();

        }
    }
}
