﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace ObligatorioP2
{
    public class Plato : IValidacion, IComparable<Plato>//utilizamos dos interfaces, una de validacion
                                                        //para utilizar el EsValido() y
                                                        //la otra para poder utilizar el metodo CompareTo()
    {
        public int Id { get; set; }

        public static int UltimoId { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }

        public static double PrecioMinimo { get; set; } = 100;
        public int Likes { get; set; }



        public Plato()
        {
        }

        public Plato(string nombre, double precio)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Precio = precio;
            Likes = 0;
        }

        public void SumarLike() //permite sumar likes en la lista de platos
        {
            Likes++;
        }

        public bool EsValido() //permite validar si el ingreso del precio del plato y su nombre es valido
        {
            bool nombreEsValido = false;
            bool precioEsValido = false;
            bool esValido = false;

            if (Nombre != "")
            {
                nombreEsValido = true;

                for (int i = 0; i < Nombre.Length; i++)
                {
                    char caracter = Nombre[i];
                    if (Char.IsNumber(caracter))
                    {
                        nombreEsValido = false;
                    }
                }

            }
            if (Precio >= PrecioMinimo)
            {
                precioEsValido = true;
            }

            if (precioEsValido && nombreEsValido)
            {
                esValido = true;
            }
            return esValido;
        }
        public static bool ModificarPrecioMinimoPlato(double precioNuevo)
        {
            if (precioNuevo != PrecioMinimo && precioNuevo >= 0) //si el precio nuevo es validado, se cambia el precio minimo de la clase Plato
            {
                PrecioMinimo = precioNuevo;
                return true;

            }
            return false;
        }

        public override string ToString() //permite que se ponga como string el nombre y el precio
        {
            return $"{Nombre}: {Precio}.   ";
        }

        public virtual int CompareTo([AllowNull] Plato other) //Compara el nombre del plato para poder ordenarlo en la lista de platos
        {
            if (Nombre.CompareTo(other.Nombre) > 0)
            {
                return 1;
            }
            else if (Nombre.CompareTo(other.Nombre) < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        
    }


}



