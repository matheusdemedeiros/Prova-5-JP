using Prova_5_JP.ConsoleApp.Compartilhado;
using Prova_5_JP.ConsoleApp.Módulo_Contatos;
using Prova_5_JP.ConsoleApp.Módulo_Tarefa;
using System;

namespace Prova_5_JP.ConsoleApp
{
    public class Program
    {
        static Notificador notificador = new Notificador();
        static TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaGerenciamentoTarefa)
                    GerenciarCadastroTarefas(telaSelecionada, opcaoSelecionada);

                //else if (telaSelecionada is TelaCadastroReserva)
                //    GerenciarCadastroReservas(telaSelecionada, opcaoSelecionada);
            }
        }

        private static void GerenciarCadastroTarefas(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaGerenciamentoTarefa telaGerenciamentoTarefa = telaSelecionada as TelaGerenciamentoTarefa;

            if (telaGerenciamentoTarefa is null)
                return;

            if (opcaoSelecionada == "1")
                telaGerenciamentoTarefa.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaGerenciamentoTarefa.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaGerenciamentoTarefa.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaGerenciamentoTarefa.VisualizarTarefasPendentes("tela");

            if (opcaoSelecionada == "5")
                telaGerenciamentoTarefa.VisualizarTarefasConcluidas("tela");

            else if (opcaoSelecionada == "6")
                telaGerenciamentoTarefa.AdicionarItemNaTarefa();

            else if (opcaoSelecionada == "7")
                telaGerenciamentoTarefa.ConcluirItemDaTarefa();

            else if (opcaoSelecionada == "8")
                telaGerenciamentoTarefa.VisualizarItensPendentesDaTarefa("tela");

            else if (opcaoSelecionada == "9")
                telaGerenciamentoTarefa.VisualizarItensConcluidosDaTarefa();
        }

        public static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            ITelaCadastravel telaCadastroBasico = telaSelecionada as ITelaCadastravel;

            if (telaCadastroBasico is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroBasico.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroBasico.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroBasico.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroBasico.VisualizarRegistros("Tela");

            TelaGerenciamentoContato telaGerenciamentoContato = telaCadastroBasico as TelaGerenciamentoContato;

            if (telaGerenciamentoContato is null)
                return;

            if (opcaoSelecionada == "5")
                telaGerenciamentoContato.VisualizarRegistrosAgrupadosPorCargo();

            //if (opcaoSelecionada == "5")
            //    telaCadastroTarefa.AdicionarItemNaTarefa();

            //else if (opcaoSelecionada == "6")
            //    telaCadastroTarefa.ConcluirItemDaTarefa();

            //else if (opcaoSelecionada == "7")
            //    telaCadastroTarefa.VisualizarItensPendentesDaTarefa("tela");

            //else if (opcaoSelecionada == "8")
            //    telaCadastroTarefa.VisualizarItensConcluidosDaTarefa();

        }
    }
}