using Microsoft.EntityFrameworkCore;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.UITests.Helpers
{
    public class TestDataCleanup
    {

        private readonly ApplicationDbContext _context;


        public TestDataCleanup(ApplicationDbContext context)
        {
            _context = context;
        }



        public void DeleteProduct(int productId)
        {
            // Si devuelve 0 el producto ya no existe — no es un error en contexto de tests
            // el test de Delete lo elimina por UI, por lo que en cleanup no hay nada que hacer
            _context.Products
                .Where(p => p.Id == productId)
                .ExecuteDelete();
        }




    }
}