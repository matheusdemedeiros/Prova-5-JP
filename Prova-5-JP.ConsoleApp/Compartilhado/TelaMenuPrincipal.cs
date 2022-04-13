
using Prova_5_JP.ConsoleApp.Módulo_Contatos;
using Prova_5_JP.ConsoleApp.Módulo_Tarefa;
using System;

namespace Prova_5_JP.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private string opcaoSelecionada;

        // Declaração de Tarefas
        private RepositorioTarefa repositorioTarefa;

        private TelaGerenciamentoTarefa telaGerenciamentoTarefa;

        // Declaração de Contatos
        private RepositorioContato repositorioContato;

        private TelaGerenciamentoContato telaGerenciamentoContato;

        /*

        // Declaração de Revistas
        private IRepositorio<Revista> repositorioRevista;

        private TelaCadastroRevista telaCadastroRevista;

        // Declaração de Amigos
        private IRepositorio<Amigo> repositorioAmigo;
        private TelaCadastroAmigo telaCadastroAmigo;

        // Declaração de Empréstimos
        private IRepositorio<Emprestimo> repositorioEmprestimo;

        private TelaCadastroEmprestimo telaCadastroEmprestimo;

        // Declaração de Reservas
        private IRepositorio<Reserva> repositorioReserva;

        private TelaCadastroReserva telaCadastroReserva;
        */
        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioTarefa = new RepositorioTarefa();
            repositorioContato = new RepositorioContato();

            //repositorioCategoria = new RepositorioJson<Categoria>();
            //repositorioRevista = new RepositorioJson<Revista>();
            //repositorioAmigo = new RepositorioJson<Amigo>();
            //repositorioEmprestimo = new RepositorioJson<Emprestimo>();
            //repositorioReserva = new RepositorioJson<Reserva>();

            telaGerenciamentoTarefa = new TelaGerenciamentoTarefa(repositorioTarefa, notificador);
            telaGerenciamentoContato = new TelaGerenciamentoContato(repositorioContato, notificador);

            //telaCadastroCategoria = new TelaCadastroCategoria(repositorioCategoria, notificador);
            //telaCadastroRevista = new TelaCadastroRevista(
            //    telaCadastroCategoria,
            //    repositorioCategoria,
            //    telaCadastroCaixa,
            //    repositorioCaixa,
            //    repositorioRevista,
            //    notificador
            //);

            //    telaCadastroAmigo = new TelaCadastroAmigo(repositorioAmigo, notificador);

            //    telaCadastroEmprestimo = new TelaCadastroEmprestimo(
            //        notificador,
            //        repositorioEmprestimo,
            //        repositorioRevista,
            //        repositorioAmigo,
            //        telaCadastroRevista,
            //        telaCadastroAmigo
            //    );

            //    telaCadastroReserva = new TelaCadastroReserva(
            //        notificador,
            //        repositorioReserva,
            //        repositorioAmigo,
            //        repositorioRevista,
            //        telaCadastroAmigo,
            //        telaCadastroRevista,
            //        repositorioEmprestimo
            //    );
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Prova 5 - JP");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Tarefas");
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

            //else if (opcao == "3")
            //    //tela = telaGerenciametoCompromissos;

            return tela;
        }
    }
}
