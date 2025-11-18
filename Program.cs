using System;
using System.IO;
using System.Windows.Forms;
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
            ApplicationConfiguration.Initialize();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string rutaUsuariosTxt = Path.Combine(basePath, "usuarios.txt");

            var repoUsuarios = new UsuarioFileRepository(rutaUsuariosTxt);
            var usuarioController = new UsuarioController(repoUsuarios);

            WinFormsApplication.Run(new FrmLogin(usuarioController));
        }
    }
}



