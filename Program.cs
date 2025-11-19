using System;
using System.IO;
using FeriaEnLinea.Application.Controllers;
using FeriaEnLinea.Infrastructure.Repositories;
using FeriaEnLinea.Presentation.Views;
using WinFormsApplication = System.Windows.Forms.Application;


namespace FeriaEnLinea.Presentation
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            WinFormsApplication.EnableVisualStyles();
            WinFormsApplication.SetCompatibleTextRenderingDefault(false);
           

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string rutaUsuariosTxt = Path.Combine(basePath, "usuarios.txt");
            string rutaProductosCsv = Path.Combine(basePath, "productos.csv");

            var repoUsuarios = new UsuarioFileRepository(rutaUsuariosTxt);
            var repoProductos = new ProductoCsvRepository(rutaProductosCsv);
            var repoCarritos = new CarritoInMemoryRepository();

            var usuarioController = new UsuarioController(repoUsuarios);
            var carritoController = new CarritoController(repoCarritos, repoProductos);

           
            using (var frmLogin = new FrmLogin(usuarioController, carritoController))
            {
                var resultado = frmLogin.ShowDialog();

                if (resultado == System.Windows.Forms.DialogResult.OK &&
                    frmLogin.UsuarioAutenticado != null)
                {
                    WinFormsApplication.Run(
                        new FrmPrincipal(frmLogin.UsuarioAutenticado, carritoController));
                }
              
            }
        }
    }
}

