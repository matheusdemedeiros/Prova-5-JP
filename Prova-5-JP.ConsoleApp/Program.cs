using Prova_5_JP.ConsoleApp.Compartilhado;
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

                //else if (telaSelecionada is TelaCadastroEmprestimo)
                //    GerenciarCadastroEmprestimos(telaSelecionada, opcaoSelecionada);

                //else if (telaSelecionada is TelaCadastroReserva)
                //    GerenciarCadastroReservas(telaSelecionada, opcaoSelecionada);
            }
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

            TelaGerenciamentoTarefa telaCadastroTarefa = telaCadastroBasico as TelaGerenciamentoTarefa;

            if (telaCadastroTarefa is null)
                return;

            if (opcaoSelecionada == "5")
                telaCadastroTarefa.AdicionarItemNaTarefa();

            else if (opcaoSelecionada == "6")
                telaCadastroTarefa.ConcluirItemDaTarefa();
            
            else if (opcaoSelecionada == "7")
                telaCadastroTarefa.VisualizarItensPendentesDaTarefa("tela");
            
            else if (opcaoSelecionada == "8")
                telaCadastroTarefa.VisualizarItensConcluidosDaTarefa();

        }
    }
}