using Prova_5_JP.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova_5_JP.ConsoleApp.Módulo_Tarefa
{
    public class Item : EntidadeBase
    {
        private readonly string descricao;
        private Status statusItem;

        public Item(string descricao)
        {
            this.descricao = descricao;
            statusItem = Status.pendente;
        }

        public bool EstaPendente => statusItem == Status.pendente ? true : false;

        public void Concluir()
        {
            if (statusItem == Status.pendente)
                statusItem = Status.concluido;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(descricao))
                erros.Add("É necessário inserir uma descrição para o item!");

            return new ResultadoValidacao(erros);
        }


        public override string ToString()
        {
            return
                "\nID: " + id +
                "\nDescrição: " + descricao +
                "\nStatus: " + statusItem;
        }
    }
}
