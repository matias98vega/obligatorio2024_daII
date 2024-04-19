using GestionEdificios.DataAccess;
using GestionEdificios.Domain;

namespace GestionEdificios.DataAccess.Tests;

[TestClass]
public class TestsRepositorioUsuario
{
    [TestMethod]
    public void TestAgregarUsuario()
    {
        var contexto = ContextoFactory.GetMemoryContext(Guid.NewGuid().ToString());
        UsuarioRepositorio usuariorepo = new UsuarioRepositorio(contexto);

        Random numeroAleatorio = new Random();
        int idUsuario = numeroAleatorio.Next();

        usuariorepo.Agregar(new Usuario
        {
            Id = idUsuario,
            Nombre = "NombreUsuario1",
            Apellido = "ApellidoUsuario1",
            Email = "Usuario1@hotmail.com",
            Contrase�a = "Contrase�aUsuario1",
            Rol = Domain.Enumerados.Roles.Administrador
        });

        usuariorepo.Salvar();
        var usuarios = usuariorepo.ObtenerTodos().ToList();

        Assert.AreEqual(usuarios.Count, 1);
        Assert.AreEqual(usuarios[0].Id, idUsuario);
    }

    [TestMethod]
    public void TestEliminarUsuario()
    {
        var contexto = ContextoFactory.GetMemoryContext(Guid.NewGuid().ToString());
        UsuarioRepositorio usuariorepo = new UsuarioRepositorio(contexto);

        Random numeroAleatorio = new Random();
        int idUsuario = numeroAleatorio.Next();

        Usuario usuario = new Usuario
        {
            Id = idUsuario,
            Nombre = "NombreUsuario1",
            Apellido = "ApellidoUsuario1",
            Email = "Usuario1@hotmail.com",
            Contrase�a = "Contrase�aUsuario1",
            Rol = Domain.Enumerados.Roles.Administrador
        };

        usuariorepo.Agregar(usuario);
        usuariorepo.Salvar();
        usuariorepo.Borrar(usuario);
        usuariorepo.Salvar();

        var usuarios = usuariorepo.ObtenerTodos().ToList();

        Assert.AreEqual(usuarios.Count, 0);
    }

    [TestMethod]
    public void TestActualizarUsuario()
    {
        var contexto = ContextoFactory.GetMemoryContext(Guid.NewGuid().ToString());
        UsuarioRepositorio usuariorepo = new UsuarioRepositorio(contexto);

        Random numeroAleatorio = new Random();
        int idUsuario = numeroAleatorio.Next();

        Usuario usuario = new Usuario
        {
            Id = idUsuario,
            Nombre = "NombreUsuario1",
            Apellido = "ApellidoUsuario1",
            Email = "Usuario1@hotmail.com",
            Contrase�a = "Contrase�aUsuario1",
            Rol = Domain.Enumerados.Roles.Administrador
        };

        usuariorepo.Agregar(usuario);
        usuariorepo.Salvar();

        var cambioContrase�a = "Contrase�aNueva";
        usuario.Contrase�a = cambioContrase�a;

        usuariorepo.Actualizar(usuario);
        usuariorepo.Salvar();

        var usuarios = usuariorepo.ObtenerTodos().ToList();
        Usuario usuarioEsperado = usuariorepo.Obtener(idUsuario);
        Assert.AreEqual(usuarioEsperado, usuario);
        Assert.AreEqual(usuarios[0].Contrase�a, cambioContrase�a);
    }

    [TestMethod]
    public void TestObtenerPorEmail()
    {
        var contexto = ContextoFactory.GetMemoryContext(Guid.NewGuid().ToString());
        UsuarioRepositorio usuariorepo = new UsuarioRepositorio(contexto);

        Random numeroAleatorio = new Random();
        int idUsuario = numeroAleatorio.Next();

        Usuario usuario = new Usuario
        {
            Id = idUsuario,
            Nombre = "NombreUsuario1",
            Apellido = "ApellidoUsuario1",
            Email = "Usuario1@hotmail.com",
            Contrase�a = "Contrase�aUsuario1",
            Rol = Domain.Enumerados.Roles.Administrador
        };

        usuariorepo.Agregar(usuario);
        usuariorepo.Salvar();

        var usuarios = usuariorepo.ObtenerTodos().ToList();
        Usuario usuarioPorEmail = usuariorepo.ObtenerPorEmail("Usuario1@hotmail.com");
        Assert.AreEqual(usuarioPorEmail, usuario);
    }
}