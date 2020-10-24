using Microsoft.EntityFrameworkCore;
using PedidosSuplidores.DAL;
using PedidosSuplidores.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PedidosSuplidores.BLL
{
    public class ProductosBLL
    {
        public static bool Guardar(Productos producto)
        {
            if (!Existe(producto.ProductoId))
                return Insertar(producto);
            else
                return Modificar(producto);
        }

        private static bool Insertar(Productos producto)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Productos.Add(producto);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        private static bool Modificar(Productos producto)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(producto).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
               
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var producto = contexto.Productos.Find(id);
                if (producto != null)
                {
                    contexto.Productos.Remove(producto);
                    paso = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Productos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Productos producto = new Productos();

            try
            {
                producto = contexto.Productos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return producto;
        }

        /*public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> criterio)
        {
            List<Ordenes> Lista = new List<Ordenes>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Moras.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }*/
        public static List<Productos> GetList()
        {
            List<Productos> lista = new List<Productos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Productos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Productos.Any(O => O.ProductoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

    }
}
