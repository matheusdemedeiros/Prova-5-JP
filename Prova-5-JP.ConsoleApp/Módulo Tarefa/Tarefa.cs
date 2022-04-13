using System;
using System.Collections.Generic;
using Prova_5_JP.ConsoleApp.Compartilhado;

namespace Prova_5_JP.ConsoleApp.Módulo_Tarefa
{
    public class Tarefa : EntidadeBase
    {

        #region Atributos

        private readonly string titulo;
        private readonly string prioridade;
        private Status statusTarefa;
        private DateTime dataConclusao;
        public DateTime dataCriacao;
        public List<Item> itensPendentes = new List<Item>();
        public List<Item> itensConcluidos = new List<Item>();

        #endregion

        #region Propriedades

        public int QuantidadeDeItensTotais => itensPendentes.Count + itensConcluidos.Count;

        public string DataConclusao => StatusTarefa == Status.concluido ? dataConclusao.ToShortDateString() : "Indeterminado";

        public int QuantidadeDeItensPendentes => itensPendentes.Count;

        public int QuantidadeDeItensConcluidos => itensConcluidos.Count;

        public string PercentualConclusao
        {
            get
            {
                if (QuantidadeDeItensTotais > 0)
                    return ((100 * QuantidadeDeItensConcluidos) / QuantidadeDeItensTotais).ToString("N2") + " %";
                else
                    return "Indeterminado";
            }
        }
        
        public Status StatusTarefa { get => statusTarefa; }

        #endregion

        public Tarefa(string tituloTarefa, string dataCriacao, string prioridade)
        {
            this.titulo = tituloTarefa;
            this.dataCriacao = DateTime.TryParse(dataCriacao, out DateTime data) ? data : new DateTime(1, 1, 1);
            this.prioridade = prioridade;

        }
        public Tarefa(string tituloTarefa, string prioridade)
        {
            this.titulo = tituloTarefa;
            this.prioridade = prioridade;
        }
        public Item RetornarItemDaListaDePendentes(int id)
        {
            return itensPendentes.Find(x => x.id == id);
        }

        public void AtualizarItensConcluidos()
        {

            itensConcluidos.AddRange(itensPendentes.FindAll(x => !x.EstaPendente));
            itensPendentes.RemoveAll(x => !x.EstaPendente);

            //foreach (Item item in itensPendentes)
            //{
            //    if (!item.EstaPendente)
            //    {
            //        itensConcluidos.Add(item);
            //        itensPendentes.Remove(item);
            //    }
            //}
        }

        public void AtualizarTarefa()
        {
            AtualizarItensConcluidos();

            if (PercentualConclusao == "100 %" && QuantidadeDeItensPendentes == 0 && QuantidadeDeItensPendentes > 0)
                ConcluirTarefa();
        }

        public void AdicionarItem(Item item)
        {
            itensPendentes.Add(item);
        }

        public override string ToString()
        {
            return
            "ID: " + id + Environment.NewLine +
            "Título: " + titulo + Environment.NewLine +
            "Data de criação: " + dataCriacao.ToShortDateString() + Environment.NewLine +
            "Data de conclusão: " + DataConclusao + Environment.NewLine +
            "Prioridade: " + prioridade.ToString() + Environment.NewLine +
            "QTD de itens: " + QuantidadeDeItensTotais + Environment.NewLine +
            "QTD de itens pendentes: " + QuantidadeDeItensPendentes + Environment.NewLine +
            "QTD de itens concluídos: " + QuantidadeDeItensConcluidos + Environment.NewLine +
            "Percentual de conclusao: " + PercentualConclusao + Environment.NewLine;
        }
        
        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(titulo))
                erros.Add("É necessário inserir um título para as tarefas!");

            if (dataCriacao.Date == new DateTime(1, 1, 1))
                erros.Add("É necessário inserir uma data de criação válida para as tarefas!");

            if (prioridade == "INVALIDO")
                erros.Add("É necessário inserir uma prioridade válida (alta, normal, baixa) para as tarefas!");

            return new ResultadoValidacao(erros);
        }

        public void MostrarItensPendentes()
        {
            foreach (Item item in itensPendentes)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
        }
        
        public void MostrarItensConcluidos()
        {
            foreach (Item item in itensConcluidos)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
        }

        #region Métodos privados

        private void ConcluirTarefa()
        {
            dataConclusao = DateTime.Now;
            statusTarefa = Status.concluido;
        }

    
        #endregion
    }
}
