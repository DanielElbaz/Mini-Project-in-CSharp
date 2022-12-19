﻿using DO;
//using Dal;
namespace DalApi
{

    public interface ICrud<T> where T : struct
    {
        int Add(T item);
        T GetByID(int id);
        void Update(int id, T item);
        void Delete(int id);
        IEnumerable<T> GetAll(/*Func<T?,bool>? filter=null*/);
    }
}