using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.InterfacesRepositorios
{
    public interface IRepositorio<T> where T : class
    {
        bool Add(T obj);
        bool Remove(int clave);
        bool Update(T obj);
        T FindById(int clave);
        List<T> FindAll();
    }
}
