using System;
using System.Collections.Generic;

namespace CarRent
{
    interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetAll(); // получение всех объектов       
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
