using Microsoft.AspNetCore.Mvc;
using Prueba_Alpha.Models;
using System.Diagnostics;
using Prueba_Alpha.ClasesDAL;
using Prueba_Alpha.Class;

namespace Prueba_Alpha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        //crear metodo para obtener los productos
        [HttpPost]
        public ActionResult getProductos() {
        
            Variables objvariables = new Variables();
            CrudAlpha crudAlpha = new CrudAlpha();

            List<Productos> productos = crudAlpha.ObtenerDatos();
            objvariables.LstProduct = productos;
            return Json(objvariables);
            
        }

        [HttpPost]
        public ActionResult getCategorias()
        {

            Variables objvariables = new Variables();
            CrudAlpha crudAlpha = new CrudAlpha();

            List<Categorias> categoria = crudAlpha.ObtenerDatosCategorias();
            objvariables.LstCategorias = categoria;
            return Json(objvariables);

        }


        public class nuevosProductos() {
            public string nombre { get; set;}
            public string categoria { get; set; }
        }

        [HttpPost]
        public ActionResult AgregarProducto([FromBody] nuevosProductos producto)
        {

            Variables objvariables = new Variables();
            CrudAlpha crudAlpha = new CrudAlpha();

            if (producto != null)
            {
                int insert = crudAlpha.AgregarProducto(producto.nombre, producto.categoria);

                objvariables.insertproducto = insert;
            }

            return Json(objvariables);

        }


        public class EditarProducto()
        {
            public string id { get; set; }
            public string nombre { get; set; }
            public string categoria { get; set; }
        }

        [HttpPost]
        public ActionResult EditProducto([FromBody] EditarProducto producto)
        {

            Variables objvariables = new Variables();
            CrudAlpha crudAlpha = new CrudAlpha();

            if (producto != null)
            {
                int editar = crudAlpha.EditarProducto(Convert.ToInt32(producto.id), producto.nombre, producto.categoria);

                objvariables.Editarproducto = editar;
            }

            return Json(objvariables);

        }

        public class varliminarProducto()
        {
            public string id { get; set; }
           
        }

        [HttpPost]
        public ActionResult EliminarProducto([FromBody] varliminarProducto producto)
        {

            Variables objvariables = new Variables();
            CrudAlpha crudAlpha = new CrudAlpha();

            if (producto != null)
            {
                int eliminar = crudAlpha.EliminarProducto(Convert.ToInt32(producto.id));

                objvariables.EliminarProducto = eliminar;
            }

            return Json(objvariables);

        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }



    public class Variables {
        public List<Productos> LstProduct {get; set;}
        public List<Categorias> LstCategorias { get; set; }
        public int insertproducto { get; set; }
        public int Editarproducto { get; set; }
        public int EliminarProducto { get; set; }
    }
}
