﻿using System.Collections.Generic;
using System.Text;
using Prova_5_JP.ConsoleApp.Compartilhado;

namespace Prova_5_JP.ConsoleApp.Compartilhado
{
    public class ResultadoValidacao
    {
        public ResultadoValidacao(List<string> erros)
        {
            _erros = erros;
        }

        private readonly List<string> _erros;

        public StatusValidacao Status
        {
            get
            {
                return _erros.Count == 0 ? StatusValidacao.Ok : StatusValidacao.Erro;
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string erro in _erros)
            {
                if (!string.IsNullOrEmpty(erro))
                    sb.Append(erro);
            }

            return sb.ToString();
        }
    }
}