using System;

namespace Prova_5_JP.ConsoleApp.Compartilhado
{
    public abstract class TelaBase
    {
        protected string Titulo { get; set; }

        public TelaBase(string titulo)
        {
            Titulo = titulo;
        }

        public virtual string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("\nDigite 1 para Inserir");
            Console.WriteLine("\nDigite 2 para Editar");
            Console.WriteLine("\nDigite 3 para Excluir");
            Console.WriteLine("\nDigite 4 para Visualizar");
            Console.WriteLine("\nDigite s para Sair");
            Console.Write("\nSua escolha: ");

            string opcao = Console.ReadLine();

            return opcao;
        }

        protected void MostrarTitulo(string titulo)
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine();
        }
    }
}
