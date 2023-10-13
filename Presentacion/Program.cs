using LogicaNegocios;

Class1 clase = new Class1();
List<string> clientes = clase.ObtenerClientesPorNombre("Juan");
foreach (string cliente in clientes)
{
    Console.WriteLine(cliente);
}