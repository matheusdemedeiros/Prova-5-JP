using Newtonsoft.Json;
using Prova_5_JPCompartilhado;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prova_5_JP.ConsoleApp.Compartilhado
{
    public class RepositorioJson<T> : ISerializavel<T>, IRepositorio<T> where T : EntidadeBase
    {
        #region Propriedades

        public string Diretorio { get; init; }
        public string Arquivo { get; init; }

        #endregion

        #region Construtor

        public RepositorioJson()
        {
            Diretorio = Environment.CurrentDirectory + "\\Dados";

            Arquivo = Diretorio + $"\\{typeof(T).Name}.json";

            if (!Directory.Exists(Diretorio))
                Directory.CreateDirectory(Diretorio);

            if (!File.Exists(Arquivo))
                File.Create(Arquivo).Close();
        }

        #endregion

        #region Métodos públicos

        public string Inserir(T entidade)
        {
            ResultadoValidacao validacao = entidade.Validar();

            if (validacao.Status == StatusValidacao.Erro)
                return validacao.ToString();

            Serializar(entidade);

            return "REGISTRO_VALIDO";
        }

        public bool Editar(Predicate<T> condicao, T novaEntidade)
        {
            List<T> registros = Deserializar();

            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                {
                    novaEntidade.id = entidade.id;

                    int posicaoParaEditar = registros.IndexOf(entidade);
                    registros[posicaoParaEditar] = novaEntidade;

                    SerializarLista(registros);
                    return true;
                }
            }

            return false;
        }

        public bool Excluir(Predicate<T> condicao)
        {
            List<T> registros = Deserializar();

            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                {
                    registros.Remove(entidade);

                    SerializarLista(registros);
                    return true;
                }
            }

            return false;
        }

        public bool ExisteRegistro(Predicate<T> condicao)
        {
            List<T> registros = Deserializar();

            foreach (T entidade in registros)
                if (condicao(entidade))
                    return true;

            return false;
        }

        public List<T> Filtrar(Predicate<T> condicao)
        {
            List<T> registros = Deserializar();

            List<T> registrosFiltrados = new List<T>();

            foreach (T entidade in registros)
                if (condicao(entidade))
                    registrosFiltrados.Add(entidade);

            return registrosFiltrados;
        }

        public T SelecionarRegistro(Predicate<T> condicao)
        {
            List<T> registros = Deserializar();

            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                    return entidade;
            }

            return null;
        }

        public List<T> SelecionarTodos()
        {
            return Deserializar();
        }

        public void Serializar(T entidade)
        {
            List<T> registros = Deserializar();

            if (registros.Count > 0)
                entidade.id = registros.OrderBy(x => x.id).Last().id + 1;
            else
                entidade.id = 1;

            registros.Add(entidade);

            string jsonRegistrosAtualizados = JsonConvert.SerializeObject(registros, Formatting.Indented);

            File.WriteAllText(Arquivo, jsonRegistrosAtualizados);
        }

        public void SerializarLista(List<T> registros)
        {
            string jsonRegistrosAtualizados = JsonConvert.SerializeObject(registros, Formatting.Indented);

            File.WriteAllText(Arquivo, jsonRegistrosAtualizados);
        }

        public List<T> Deserializar()
        {
            string jsonString = File.ReadAllText(Arquivo);

            if (string.IsNullOrEmpty(jsonString))
                return new List<T>();

            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }
        
        #endregion
    }
}
