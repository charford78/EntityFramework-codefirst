using SalesDbLib.Controllers;
using System;
using System.Threading.Tasks;

namespace EntityFramework_codefirst
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var custCtrl = new CustomersController();
            var customers = await custCtrl.GetAll();
            customers.ForEach(c => Console.WriteLine(c.Name));
        }
    }
}
