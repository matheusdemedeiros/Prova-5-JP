
using Prova_5_JP.ConsoleApp.Módulo_Contatos;
using Prova_5_JP.ConsoleApp.Módulo_Tarefa;
using Prova_5_JP.ConsoleApp.Módulo_Compromissos;
using System;

namespace Prova_5_JP.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        #region Atributos

        private string opcaoSelecionada;

        // Declaração de Tarefas
        private RepositorioTarefa repositorioTarefa;

        private TelaGerenciamentoTarefa telaGerenciamentoTarefa;

        // Declaração de Contatos
        private RepositorioContato repositorioContato;

        private TelaGerenciamentoContato telaGerenciamentoContato;


        // Declaração de Compromissos
        private RepositorioCompromisso repositorioCompromisso;

        private TelaGerenciamentoCompromisso telaGerenciamentoCompromisso;

        #endregion

        #region Construtor

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioTarefa = new RepositorioTarefa();
            repositorioContato = new RepositorioContato();
            repositorioCompromisso = new RepositorioCompromisso();

            telaGerenciamentoTarefa = new TelaGerenciamentoTarefa(repositorioTarefa, notificador);
            telaGerenciamentoContato = new TelaGerenciamentoContato(repositorioContato, notificador);
            telaGerenciamentoCompromisso = new TelaGerenciamentoCompromisso(notificador,
                repositorioCompromisso,
                repositorioContato,
                telaGerenciamentoContato);
        }
        
        #endregion

        #region Métodos públicos

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n\tProva 5 - JP");

            Console.ResetColor();

            Console.WriteLine();

            Console.WriteLine("\nDigite 1 para Gerenciar Tarefas");
            Console.WriteLine("\nDigite 2 para Gerenciar Contatos");
            Console.WriteLine("\nDigite 3 para Gerenciar Comprpmissos");
            Console.WriteLine("\nDigite s para sair");
            Console.Write("\nSua escolha: ");

            opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaGerenciamentoTarefa;

            else if (opcao == "2")
                tela = telaGerenciamentoContato;

            else if (opcao == "3")
                tela = telaGerenciamentoCompromisso;

            return tela;
        }
        
        #endregion
    }
}
