using System;
using System.Collections.Generic;
using System.Linq;

namespace Prova_5_JP.ConsoleApp.Compartilhado
{
    public class RepositorioBase<T> where T : EntidadeBase
    {
        protected readonly List<T> registros;

        protected int contadorId;

        public RepositorioBase()
        {
            registros = new List<T>();
        }

        public virtual string Inserir(T entidade)
        {
            ResultadoValidacao validacao = entidade.Validar();

            if (validacao.Status == StatusValidacao.Erro)
                return validacao.ToString();

            entidade.id = ++contadorId;

            registros.Add(entidade);

            return "REGISTRO_VALIDO";
        }

        public bool Editar(int idSelecionado, T novaEntidade)
        {
            return Editar(x => x.id == idSelecionado, novaEntidade);
        }

        public bool Editar(Predicate<T> condicao, T novaEntidade)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                {
                    novaEntidade.id = entidade.id;

                    int posicaoParaEditar = registros.IndexOf(entidade);
                    registros[posicaoParaEditar] = novaEntidade;

                    return true;
                }
            }

            return false;
        }

        public bool Excluir(int idSelecionado)
        {
            return Excluir(x => x.id == idSelecionado);
        }

        public bool Excluir(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                {
                    registros.Remove(entidade);
                    return true;
                }
            }
            return false;
        }

        public T SelecionarRegistro(int idSelecionado)
        {
            return SelecionarRegistro(x => x.id == idSelecionado);
        }

        public T SelecionarRegistro(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                    return entidade;
            }

            return null;
        }

        public List<T> SelecionarTodos()
        {
            return registros;
        }

        public List<T> Filtrar(Predicate<T> condicao)
        {
            List<T> registrosFiltrados = new List<T>();

            foreach (T registro in registros)
                if (condicao(registro))
                    registrosFiltrados.Add(registro);

            return registrosFiltrados;
        }

        public bool ExisteRegistro(int idSelecionado)
        {
            return ExisteRegistro(x => x.id == idSelecionado);
        }

        public bool ExisteRegistro(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
                if (condicao(entidade))
                    return true;
                
            return false;
        }
    }
}
