using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        private static List<int> objResult;
        static void Main(string[] args)
        {

            start();
            Console.WriteLine("Presione cualquier tecla para terminar.");
            Console.ReadLine();
        }

        #region metodos privados
        private static void start()
        {
            try
            {
                Console.WriteLine("Por favor ingresar valor N");
                int n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Por favor ingresar valor M");
                int m = Convert.ToInt32(Console.ReadLine());
                if (!validate(n, m))
                {
                    Console.WriteLine("No supero las validaciones");
                    return;
                }
                objResult = new List<int>();
                Console.WriteLine("Por favor ingresar contenido de la lista A");
                string content_a = Console.ReadLine();
                bool sw = true;
                List<result> A = get_object_list(content_a,n,ref sw);
                if (!sw)
                    return;
                Console.WriteLine("Por favor ingresar contenido de la lista B");
                string content_b = Console.ReadLine();
                sw = true;
                List<result> B = get_object_list(content_b,m,ref sw);
                if (!sw)
                    return;
                set_objResult(A, B);
                set_objResult(B, A);

                Console.WriteLine("Result :");
                Console.Write(string.Join(",", objResult));
                Console.WriteLine(Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private static bool validate(int n, int m)
        {
            return true;
            if (n >= 1 && m <= 2 * Math.Pow(10, 5))
                return false;
            if (n <= m)
                return false;
            return true;
        }
        private static List<result> get_object_list(string objrequest,int value,ref bool sw)
        {
            sw = true;
            List<result> objresponse = new List<result>();
            try
            {
                if (string.IsNullOrEmpty(objrequest))
                    return objresponse;
                string[] vec = objrequest.Split((char)32);
                foreach (string i in vec)
                    objresponse.Add(new result() { item = Convert.ToInt32(i) });

                if (objresponse.Count != value)
                {
                    Console.WriteLine("el valor de n (" + value.ToString() + ") no concuerda con el los valores ingresados (" +objrequest.Count() + ")");
                    sw = false;
                }

                return objresponse = objresponse.GroupBy(info => new { info.item })
                      .Select(group => new result()
                      {
                          item = group.Key.item,
                          count = group.Count()
                      }).ToList();
            }
            catch (Exception)
            {
                sw = false;
                return new List<result>();
            }
        }
        private static void set_objResult(List<result> obj1, List<result> obj2)
        {
            foreach (result o in obj1)
            {
                if (!obj2.Any(cus => cus.item == o.item && cus.count == o.count))
                    if (!objResult.Any(cus => cus == o.item))
                        objResult.Add(Convert.ToInt32(o.item));
            }
        }
        #endregion
    }

    public class result
    {
        public int item { get; set; }
        public int count { get; set; }
    }
}
