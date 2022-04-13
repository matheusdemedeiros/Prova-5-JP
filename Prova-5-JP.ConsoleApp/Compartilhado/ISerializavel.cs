using Prova_5_JP.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace Prova_5_JPCompartilhado
{
    public interface ISerializavel<T> where T : EntidadeBase
    {
        void Serializar(T entidade);
        void SerializarLista(List<T> entidade);
        List<T> Deserializar();
    }
}